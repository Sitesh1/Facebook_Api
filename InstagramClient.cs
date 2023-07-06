using InstaSharp;
using System;
using System.Threading.Tasks;

namespace Facebook_Integration
{
    internal class InstagramClient
    {
        private InstagramConfig config;

        public InstagramClient(InstagramConfig config)
        {
            this.config = config;
        }

        internal Task GetMediaByIdAsync(string mediaId)
        {
            throw new NotImplementedException();
        }
    }
}