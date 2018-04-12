using System.Net;
using FluentSiren.Builders;

namespace CourseAPI.Responses.Storyboard {
    public class StoryboardResponse : BaseSirenEntity {
        private readonly Models.Storyboard _storyboard;

        public StoryboardResponse(Models.Storyboard storyboard) { 
            _storyboard = storyboard;
            this.WithClass("storyboard")
                .WithProperty("id", storyboard.StoryboardId)
                .WithProperty("course", storyboard.CourseId)
                .WithProperty("active", storyboard.Active)
                .WithProperty("document", WebUtility.HtmlDecode(storyboard.Document))
                .WithProperty("readForReview", storyboard.ReadyForReview)
                .WithProperty("complete", storyboard.Complete)
                .WithLink( new LinkBuilder()
                    .WithHref(GetBaseURL()+"storyboards/"+storyboard.StoryboardId)
                    .WithRel("self"));
        }

        public StoryboardResponse WithAddSlide() {
            this.WithAction(new ActionBuilder()
                .WithClass("slide")
                .WithName("add-slide")
                .WithTitle("Add Slide")
                .WithHref(GetBaseURL() + "storyboards/"+_storyboard.StoryboardId+"/slide")
                .WithMethod("POST")
                .WithField(new FieldBuilder()
                    .WithName("graphicsNote")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("writerNote")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("text")));
            return this;
        }

        public StoryboardResponse WithDeleteSlide() {
            this.WithAction(new ActionBuilder()
                .WithClass("slide")
                .WithName("delete-slide")
                .WithTitle("Delete Slide")
                .WithHref(GetBaseURL() + "storyboards/"+_storyboard.StoryboardId+"/slide")
                .WithMethod("DELETE")
                .WithField(new FieldBuilder()
                    .WithName("id")
                    .WithType("number")));
            return this;
        }

        public StoryboardResponse WithEditSlide() {
            this.WithAction(new ActionBuilder()
                .WithClass("slide")
                .WithName("edit-slide")
                .WithTitle("Edit Title")
                .WithHref(GetBaseURL() + "storyboards/"+_storyboard.StoryboardId+"/slide")
                .WithMethod("PATCH")
                .WithField(new FieldBuilder()
                    .WithName("graphicsNote")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("writersNote")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("file")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("text")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("audio")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("complete")
                    .WithType("boolean")));
            return this;
        }
        
    }
}