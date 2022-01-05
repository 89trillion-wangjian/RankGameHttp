namespace Entity
{
    public class JsonModel
    {
        public string Id;

        public string NickName;

        public string Avatar;

        public string Trophy;

        public JsonModel(string id, string nickName, string avatar, string trophy)
        {
            this.Id = id;
            this.NickName = nickName;
            this.Avatar = avatar;
            this.Trophy = trophy;
        }
    }
}