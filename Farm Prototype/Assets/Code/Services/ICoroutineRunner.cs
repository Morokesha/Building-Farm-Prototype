namespace Code.Services
{
    public interface ICoroutineRunner
    {
         Coroutine StartCoroutine(IEnumerator coroutine);
    }
}