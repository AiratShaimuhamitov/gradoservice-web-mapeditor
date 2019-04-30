using AutoMapper;
using GradoService.Domain.Entities.File;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence.CommandBuilder;
using GradoService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradoService.Persistence
{
    public class FileRepository
    {
        private readonly GradoServiceDbContext _dbContext;
        private readonly CrudCommandDirector _commandDirector;

        public FileRepository(GradoServiceDbContext dbContext, CrudCommandDirector commandDirector)
        {
            _dbContext = dbContext;
            _commandDirector = commandDirector;
        }

        public async Task<int> InsertFile(int tableId, File file)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                                        .Include(x => x.TableInfo)
                                                        .FirstOrDefaultAsync();

            var table = CreateFileTableInstance(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);
            var fileRow = MapFileToRow(file);
            fileRow.Data.Remove(fileRow.Data.First(x => x.Key.Name == table.Key).Key);

            var insertQuery = _commandDirector.BuildInsertCommandParameterized(table, fileRow);

            var queryParameters = fileRow.Data
                                        .Where(x => x.Value != null)
                                        .Select(x => new KeyValuePair<string, object>("@" + x.Key.Name, x.Value))
                                        .ToDictionary(x => x.Key, v => v.Value);

            var insertedRowId = await _dbContext.ExecuteSqlQueryAsync(insertQuery, queryParameters);

            return insertedRowId;
        }

        public async Task UpdateFile(int tableId, File file)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                            .Include(x => x.TableInfo)
                                            .FirstOrDefaultAsync();

            var table = CreateFileTableInstance(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);
            var fileRow = MapFileToRow(file);

            var updateQuery = _commandDirector.BuildUpdateCommandParameterized(table, fileRow);

            var queryParameters = fileRow.Data
                                        .Where(x => x.Value != null)
                                        .Select(x => new KeyValuePair<string, object>("@" + x.Key.Name, x.Value))
                                        .ToDictionary(x => x.Key, v => v.Value);

            await _dbContext.ExecuteSqlQueryAsync(updateQuery, queryParameters);
        }

        public async Task DeleteFile(int tableId, int fileId)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                            .Include(x => x.TableInfo)
                                            .FirstOrDefaultAsync();

            var table = CreateFileTableInstance(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);
            var deleteQuery = _commandDirector.BuildDeleteCommand(table, fileId);

            await _dbContext.ExecuteSqlQueryAsync(deleteQuery);
        }

        public async Task<IEnumerable<File>> GetTableFiles(int tableId)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                                        .Include(x => x.TableInfo)
                                                        .FirstOrDefaultAsync();
            var table = CreateFileTableInstance(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);

            var selectQueryPhoto = _commandDirector.BuildSelectViewByName(table, tableFileMeta.ViewNamePhoto);
            var selectQueryFile = _commandDirector.BuildSelectViewByName(table, tableFileMeta.ViewNameFile);

            var unhandledRows = _dbContext.CollectFromExecuteSql(selectQueryPhoto).ToList();
            unhandledRows.AddRange(_dbContext.CollectFromExecuteSql(selectQueryFile).ToList());

            if (unhandledRows.Count == 0) return null;

            var files = new List<File>();

            foreach (var unhandledRow in unhandledRows)
            {
                var file = MapRowToFile(unhandledRow);
                files.Add(file);
            }

            return files;
        }

        public async Task<IEnumerable<File>> GetObjectFiles(int tableId, int objectId)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                                        .Include(x => x.TableInfo)
                                                        .FirstOrDefaultAsync();

            var table = CreateFileTableInstance(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);

            var selectQueryPhoto = _commandDirector.BuildSelectViewByName(table, objectId, tableFileMeta.ViewNamePhoto);
            var selectQueryFile = _commandDirector.BuildSelectViewByName(table, objectId, tableFileMeta.ViewNameFile);

            var unhandledRows = _dbContext.CollectFromExecuteSql(selectQueryPhoto).ToList();
            unhandledRows.AddRange(_dbContext.CollectFromExecuteSql(selectQueryFile).ToList());

            if (unhandledRows.Count == 0) return null;

            var files = new List<File>();

            foreach (var unhandledRow in unhandledRows)
            {
                var file = MapRowToFile(unhandledRow);
                files.Add(file);
            }

            return files;
        }

        public async Task<File> GetObjectSpecificFile(int tableId, int objectId, int fileId)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                            .Include(x => x.TableInfo)
                                            .FirstOrDefaultAsync();

            var table = CreateFileTableInstance(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);

            var selectQueryPhoto = _commandDirector.BuildSelectViewByName(table, objectId, fileId, tableFileMeta.ViewNamePhoto);
            var selectQueryFile = _commandDirector.BuildSelectViewByName(table, objectId, fileId, tableFileMeta.ViewNameFile);

            var unhandledRow = _dbContext.CollectFromExecuteSql(selectQueryPhoto).ToList();
            unhandledRow.AddRange(_dbContext.CollectFromExecuteSql(selectQueryFile).ToList());

            if (unhandledRow.Count == 0) return null;

            var file = MapRowToFile(unhandledRow.First());

            return file;
        }

        private File MapRowToFile(IDictionary<string, object> row)
        {
            var file = new File();

            foreach (var field in row.Keys)
            {
                switch (field)
                {
                    case "id":
                        file.Id = (int)row[field];
                        break;
                    case "id_obj":
                        file.ObjectId = (int)row[field];
                        break;
                    case "file":
                        file.Data = row[field] as byte[];
                        break;
                    case "img_preview":
                        file.ImagePreview = row[field] as byte[];
                        break;
                    case "file_name":
                        file.Name = (string)row[field];
                        break;
                    case "is_photo":
                        file.IsPhoto = (bool)row[field];
                        break;
                }
            }
            return file;
        }

        private Row MapFileToRow(File file)
        {
            var fields = GetFileConstantFields().ToList();

            var row = new Row();

            foreach (var field in fields)
            {
                switch (field.Name)
                {
                    case "id":
                        row.Data.Add(field, file.Id);
                        break;
                    case "id_obj":
                        row.Data.Add(field, file.ObjectId);
                        break;
                    case "file":
                        row.Data.Add(field, file.Data);
                        break;
                    case "img_preview":
                        row.Data.Add(field, file.ImagePreview);
                        break;
                    case "file_name":
                        row.Data.Add(field, file.Name);
                        break;
                    case "is_photo":
                        row.Data.Add(field, file.IsPhoto);
                        break;
                }
            }

            return row;
        }

        private IEnumerable<Field> GetFileConstantFields()
        {
            var fields = new List<Field>
            {
                new Field { Id = 0, Name = "id", Type = "INTEGER" },
                new Field { Id = 1, Name = "id_obj", Type = "INTEGER" },
                new Field { Id = 2, Name = "file", Type = "bytea"},
                new Field { Id = 3, Name = "img_preview", Type = "bytea"},
                new Field { Id = 4, Name = "file_name", Type = "character varying"},
                new Field { Id = 5, Name = "is_photo", Type = "boolean" }
            };

            return fields;
        }

        private Table CreateFileTableInstance(string schema, string tableName)
        {
            var table = new Table
            {
                Key = "id",
                Schema = schema,
                Name = tableName,
                Fields = GetFileConstantFields()
            };

            return table;
        }
    }
}
