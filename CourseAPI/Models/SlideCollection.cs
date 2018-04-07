using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CourseAPI.Models {
    public class SlideCollection : ICollection<Slide> {
        public int Id { get; set; }
        public int Count { get; }
        public bool IsReadOnly => false;

        private string _slides;
        [NotMapped]
        public List<Slide> Slides {
            get => JsonConvert.DeserializeObject<List<Slide>>(string.IsNullOrEmpty(_slides) ? "[{}]" : _slides);
            set => _slides = JsonConvert.SerializeObject(value);
        }
        
        public IEnumerator<CourseAPI.Models.Slide> GetEnumerator() {
            List<CourseAPI.Models.Slide> slides = Slides;
            return slides.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(Slide item) {
            List<Slide> slides = Slides;
            slides.Add(item);
            Slides = slides;
        }

        public void Clear() {
            List<Slide> slides = Slides;
            slides.Clear();
            Slides = slides;
        }

        public bool Contains(Slide item) {
            List<CourseAPI.Models.Slide> slides = Slides; 
            return slides.Contains(item);
        }

        public void CopyTo(Slide[] array, int arrayIndex) {
            List<Slide> slides = Slides;
            slides.CopyTo(array, arrayIndex);
            Slides = slides;
        }

        public bool Remove(Slide item) {
            List<Slide> slides = Slides;
            bool res = slides.Remove(item);
            Slides = slides;
            return res;
        }
    }
}