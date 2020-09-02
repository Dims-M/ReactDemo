using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /// <summary>
    /// Класс отвечает за сам Пост.
    /// </summary>
    public class Post
    {
        public int PostId { get; set; }
        /// <summary>
        /// загаловок
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// тело поста(контент)
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Дата публикации
        /// </summary>
        public DateTime CreatedDate { get; set; }

        //Внешние ключи для связи с другими Бд
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }


}
