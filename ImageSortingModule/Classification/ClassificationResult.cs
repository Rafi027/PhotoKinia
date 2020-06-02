using ImageSortingModule;

namespace PhotoKinia.Modules.ImageSortingModule
{
    public class ClassificationResult
    {
        public bool Success { get; set; }
        public ClassifiedRelativePath ClassifiedPath { get; set; }
    }
}