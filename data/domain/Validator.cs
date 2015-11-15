namespace email_client.data.domain
{
    public interface Validator<E>
    {
        Errors validate(E element);
    }
}