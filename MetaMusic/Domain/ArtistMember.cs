using System.Collections.Generic;

namespace MetaMusic.Domain
{
    public class ArtistMember
    {
        /// <summary>
        /// Member name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// True if active
        /// </summary>
        public bool IsActive { get; set; }
    }
}
