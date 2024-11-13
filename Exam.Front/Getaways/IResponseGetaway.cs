namespace Exam.Front.Getaways
{
    public interface IResponseGetaway
    {
        Task<string> GetAsync(string url);
        Task<string> PostAsync<T>(T t, string url);
        Task<string> PutAsync<T>(T t, string url);
    }
}
