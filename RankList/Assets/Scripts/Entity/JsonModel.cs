namespace Entity
{
    public class JsonModel
    {
        public string id;

        public string nickName;

        public string avatar;

        public string trophy;

        public JsonModel(string id, string nickName, string avatar, string trophy)
        {
            this.id = id;
            this.nickName = nickName;
            this.avatar = avatar;
            this.trophy = trophy;
        }
    }
}