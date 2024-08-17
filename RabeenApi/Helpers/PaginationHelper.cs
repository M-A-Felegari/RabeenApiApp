namespace RabeenApi.Helpers;

public static class PaginationHelper
{
    public static int CalculateTotalPages(int totalItems, int pageLength)
    {
        if (pageLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageLength), "pageLength cant be less then or equal to 1");

        if (totalItems == 0)
            return 0;
        
        return totalItems / pageLength + (pageLength != 1 ? 1 : 0);
    }
}