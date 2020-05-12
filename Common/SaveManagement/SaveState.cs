using VoxelValley.Common.Helper;

namespace VoxelValley.Common.SaveManagement
{
    public abstract class SaveState
    {
        string directory;
        string fileName;
        string extension;

        public SaveState(string directory, string fileName, string extension)
        {
            this.directory = directory;
            this.fileName = fileName;
            this.extension = extension;
        }

        public virtual void Save()
        {
            FileHelper.WriteToFileJson(directory, fileName, extension, this);
        }

        public abstract void Load();
    }
}