using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sokyu.Models
{
    public class Blob
    {
        public static async Task PutAsync(string filename, Stream stream)
        {
            var db = DbConnection.db;
            var collection = db.GetCollection<GridFSFileInfo>("fs.files");
            var bucket = new GridFSBucket(db);

            var builder = Builders<GridFSFileInfo>.Filter;
            var filter = builder.Eq(i => i.Filename, filename);
            var foo = await collection.FindAsync<GridFSFileInfo>(filter);
            var foooo = foo.ToList();
            foreach (var el in foooo)
            {
                await bucket.DeleteAsync(el.Id);
            }

            var id = await bucket.UploadFromStreamAsync(filename, stream);
        }

        public static async Task<Stream> GetAsync(string filename)
        {
            var db = DbConnection.db;
            var collection = db.GetCollection<GridFSFileInfo>("fs.files");

            var builder = Builders<GridFSFileInfo>.Filter;
            var filter = builder.Eq(i => i.Filename, filename);
            var fileCollection = await collection.FindAsync<GridFSFileInfo>(filter);
            var file = fileCollection.FirstOrDefault();
            if (file == null)
            {
                return null;
            }
            var bucket = new GridFSBucket(db);

            var stream = new MemoryStream();
            await bucket.DownloadToStreamByNameAsync(filename, stream);
            stream.Position = 0;
            return stream;

        }
    }

}
