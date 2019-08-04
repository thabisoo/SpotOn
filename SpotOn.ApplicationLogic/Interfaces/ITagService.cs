using SpotOn.ApplicationLogic.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.ApplicationLogic.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagEntity>> AddTags(Guid postId, IEnumerable<string> tags);
    }
}
