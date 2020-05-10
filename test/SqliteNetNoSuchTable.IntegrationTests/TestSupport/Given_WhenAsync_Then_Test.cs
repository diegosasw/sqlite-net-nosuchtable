using System;
using System.Threading.Tasks;

namespace SqliteNetNoSuchTable.IntegrationTests.TestSupport
{
    public abstract class Given_WhenAsync_Then_Test
        : IDisposable
    {
        protected Given_WhenAsync_Then_Test()
        {
            Task.Run(async () => { await SetupAsync(); }).GetAwaiter().GetResult();
        }

        private async Task SetupAsync()
        {
            Given();
            await WhenAsync();
        }

        protected abstract void Given();

        protected abstract Task WhenAsync();

        public void Dispose()
        {
            Cleanup();
        }

        protected virtual void Cleanup()
        {
        }
    }

}
