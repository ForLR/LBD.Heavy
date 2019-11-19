using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Application.ViewModels
{
    public class CloundMusicViewModel
    {
        public long Id { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 歌曲名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 歌手名字
        /// </summary>
        public string SingerName { get; set; }
        public string PicUrl { get; set; }

        /// <summary>
        /// 专辑
        /// </summary>
        public string SpecialName { get; set; }

        /// <summary>
        /// 专辑id
        /// </summary>
        public string Pic { get; set; }
    }
}
