using AutoMapper;
using GradoService.Domain.Entities.File;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence.CommandBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<File>> GetTableFiles(int tableId)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                                        .Include(x => x.TableInfo)
                                                        .FirstOrDefaultAsync();
            var table = GetFileTable(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);

            var selectQueryPhoto = _commandDirector.BuildSelectViewByName(table, tableFileMeta.ViewNamePhoto);
            var selectQueryFile = _commandDirector.BuildSelectViewByName(table, tableFileMeta.ViewNameFile);

            var unhandledRows = _dbContext.CollectFromExecuteSql(selectQueryPhoto).ToList();
            unhandledRows.AddRange(_dbContext.CollectFromExecuteSql(selectQueryFile).ToList());

            if (unhandledRows.Count == 0) return null;

            var files = new List<File>();

            foreach(var unhandledRow in unhandledRows)
            {
                var file = TrasnformToFile(unhandledRow);
                files.Add(file);
            }

            return files;
        }

        public async Task<IEnumerable<File>> GetObjectFiles(int tableId, int objectId)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                                        .Include(x => x.TableInfo)
                                                        .FirstOrDefaultAsync();

            var table = GetFileTable(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);

            var selectQueryPhoto = _commandDirector.BuildSelectViewByName(table, objectId, tableFileMeta.ViewNamePhoto);
            var selectQueryFile = _commandDirector.BuildSelectViewByName(table, objectId, tableFileMeta.ViewNameFile);

            var unhandledRows = _dbContext.CollectFromExecuteSql(selectQueryPhoto).ToList();
            unhandledRows.AddRange(_dbContext.CollectFromExecuteSql(selectQueryFile).ToList());

            if (unhandledRows.Count == 0) return null;

            var files = new List<File>();

            foreach (var unhandledRow in unhandledRows)
            {
                var file = TrasnformToFile(unhandledRow);
                files.Add(file);
            }

            return files;
        }

        public async Task<File> GetObjectSpecificFile(int tableId, int objectId, int fileId)
        {
            var tableFileMeta = await _dbContext.TableFiles.Where(x => x.TableId == tableId)
                                            .Include(x => x.TableInfo)
                                            .FirstOrDefaultAsync();

            var table = GetFileTable(tableFileMeta.TableInfo.Schema, tableFileMeta.TableName);

            var selectQueryPhoto = _commandDirector.BuildSelectViewByName(table, objectId, fileId, tableFileMeta.ViewNamePhoto);
            var selectQueryFile = _commandDirector.BuildSelectViewByName(table, objectId, fileId, tableFileMeta.ViewNameFile);

            var unhandledRow = _dbContext.CollectFromExecuteSql(selectQueryPhoto).ToList();
            unhandledRow.AddRange(_dbContext.CollectFromExecuteSql(selectQueryFile).ToList());

            if (unhandledRow.Count == 0) return null;

            var file = TrasnformToFile(unhandledRow.First());

            return file;
        }

        private File TrasnformToFile(IDictionary<string, object> row)
        {
            var file = new File();

            foreach (var field in row.Keys)
            {
                switch (field)
                {
                    case "id":
                        file.Id = (int) row[field];
                        break;
                    case "id_obj":
                        file.ObjectId = (int) row[field];
                        break;
                    case "file":
                        file.Data = (byte[]) row[field];
                        break;
                    case "img_preview":
                        file.ImagePreview = (byte[]) row[field];
                        break;
                    case "file_name":
                        file.Name = (string) row[field];
                        break;
                    case "is_photo":
                        file.IsPhoto = (bool) row[field];
                        break;
                }
            }
            return file;
        }

        private IEnumerable<Field> GetFileFields()
        {
            var fields = new List<Field>
            {
                new Field { Id = 0, Name = "id", Type = "INTEGER" },
                new Field { Id = 1, Name = "id_obj", Type = "INTEGER" },
                new Field { Id = 2, Name = "file", Type = "bytea"},
                new Field { Id = 3, Name = "imp_preview", Type = "bytea"},
                new Field { Id = 4, Name = "file_name", Type = "character varying"},
                new Field { Id = 5, Name = "is_photo", Type = "boolean"}
            };

            return fields;
        }

        private Table GetFileTable(string schema, string tableName)
        {
            var table = new Table
            {
                Key = "id",
                Schema = schema,
                Name = tableName,
                Fields = GetFileFields()
            };

            return table;
        }
    }
}
