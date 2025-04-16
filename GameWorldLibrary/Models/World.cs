using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using GameWorldLibrary.DesignPattern;
using GameWorldLibrary.Logger;

namespace GameWorldLibrary.Models
{
    public enum GameLevel
    {
        Novice,
        Normal,
        Trained
    }

    public class World
    {

        #region Properties

        public int MaxY { get; private set; }
        public int MaxX { get; private set; }
        public List<Creature> CreatureList { get; set; }
        public List<WorldObject> WorldObjectList { get; set; }
        public GameLevel GameLevel { get; private set; }
        private readonly MyLogger logger = null;



        #endregion

        #region Constructor



        // Constructor der automatisk læser fra configfil
        public World()
        {

            logger = MyLogger.GetInstance();

            
            CreatureList = new List<Creature>();
            WorldObjectList = new List<WorldObject>();
            LoadFromXml();


        }

        #endregion

        #region Method

        private void LoadFromXml()
        {
            XmlDocument configDoc = new XmlDocument();

            try
            {
                configDoc.Load("C:\\Users\\maria\\OneDrive\\Skrivebord\\4. Semester\\Advanced Software Construction\\Mandatory Assignment (Advanced Software Construction)\\GameWorldLibrary\\GameWordConsole\\GameConfig.xml");

                // MaxX

                XmlNode maxXNode = configDoc.DocumentElement.SelectSingleNode("//GameSettings/World/WorldSize/MaxX");
                MaxX = Convert.ToInt32(maxXNode?.InnerText.Trim());


                XmlNode maxYNode = configDoc.DocumentElement.SelectSingleNode("//GameSettings/World/WorldSize/MaxY");
                MaxY = Convert.ToInt32(maxYNode?.InnerText.Trim());



                // GameLevel

                XmlNode levelNode = configDoc.DocumentElement.SelectSingleNode("//GameSettings/World/GameLevel");
                string GameLevelText = levelNode.InnerText.Trim();
                GameLevel = (GameLevel)Enum.Parse(typeof(GameLevel), GameLevelText);

                logger.LogInfo($"Max X is {MaxX}, Max Y is {MaxY} and Game level is {GameLevel}");

            }
            catch (Exception ex)
            {

                MaxX = 50;
                MaxY = 50;
                GameLevel = GameLevel.Normal;
                logger.LogError("Fejl ved indlæsning af konfigurationsfil: " + ex.Message);
            }
        }


        #endregion
    }
}
