using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using StackExchange.Redis.Extensions;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Protobuf;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MyContactService.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ICacheClient client = null;
        public string DBKey
        {
            get;
        }

        public Repository(IDatabase database)
        {
            var serializer = new ProtobufSerializer();
            this.DBKey = typeof(T).FullName;
            this.client = new StackExchangeRedisCacheClient(database.Multiplexer, serializer);
        }

        #region IRepository<T> Members

               

        public IEnumerable<T> GetAll()
        {
            return client.GetAll<T>(client.SearchKeys(DBKey)).Select(x=>x.Value).AsEnumerable();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var all = client.GetAll<MyContactService.Repo.Models.Contact>(client.SearchKeys(string.Concat(DBKey, "*"))).ToList();
            return await Task.Run(() => client.GetAll<T>(client.SearchKeys(string.Concat(DBKey, "*"))).Select(x => x.Value).AsEnumerable());
        }

        public bool Insert(string id, T entity)
        {
            return client.Add<T>(string.Format("{0}:{1}", DBKey, id), entity);
        }

        public bool Delete(string id)
        {
            return client.Remove(string.Format("{0}:{1}", DBKey, id));
        }

        public T GetById(string id)
        {
            return client.Get<T>(string.Format("{0}:{1}", DBKey, id));
        }

        public bool Update(string id, T entity)
        {
            return client.Replace<T>(string.Format("{0}:{1}", DBKey, id), entity);
        }

        public bool Commit()
        {
            client.Save(SaveType.BackgroundSave);
            return true;
        }

        public async Task<bool> InsertAsync(string id, T entity)
        {
            return await client.AddAsync<T>(string.Format("{0}:{1}", DBKey, id), entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await client.RemoveAsync(string.Format("{0}:{1}", DBKey, id));
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await client.GetAsync<T>(string.Format("{0}:{1}", DBKey, id));
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            return await client.ReplaceAsync<T>(string.Format("{0}:{1}", DBKey, id), entity);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //client.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }



        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }
}