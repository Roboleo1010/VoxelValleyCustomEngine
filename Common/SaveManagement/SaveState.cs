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
            //TODO: replace 0 with &nbsp so 0,0,0,1,0 0> ,,,1,, to save disk space
            FileHelper.WriteToFileJson(directory, fileName, extension, this);
        }

        public abstract void Load();
    }
}