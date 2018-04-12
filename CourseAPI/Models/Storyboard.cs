using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CourseAPI.Data.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CourseAPI.Models
{
    public class Storyboard : BaseModel
    {
        public int StoryboardId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public string Document { get; set; }
        
        public bool Active { get; set; }

        private string _graphics;
        [NotMapped]
        public List<IFormFile> Graphics {
            get => JsonConvert.DeserializeObject<List<IFormFile>>(string.IsNullOrEmpty(_graphics) ? "[{}]" : _graphics);
            set => _graphics = JsonConvert.SerializeObject(value);
        }
        public bool GraphicsReadyForReview { get; set; }
        public bool GraphicsComplete { get; set; }
        
        private string _narration;
        [NotMapped]
        public List<IFormFile> Narration {
            get => JsonConvert.DeserializeObject<List<IFormFile>>(string.IsNullOrEmpty(_narration) ? "[{}]" : _narration);
            set => _narration = JsonConvert.SerializeObject(value);
        }
        public bool NarrationReadyForReview { get; set; }
        public bool NarrationComplete { get; set; }
        
        public bool ReadyForReview { get; set; }
        public bool Complete { get; set; }
    }
}