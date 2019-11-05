using Heavy.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Model
{
    public class Album : Entity
    {
        public Album(string name,string title ,string imgUrl)
        {
            this.Name = name;
            this.Title = title;
            this.ImgUrl = imgUrl;

        }
        public string Name { get;private set; }

        public string Title { get;private set; }

        public string ImgUrl { get; private set; }

    }
}
