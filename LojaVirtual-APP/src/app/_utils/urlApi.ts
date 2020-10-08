export class UrlApi {
    private static readonly UrlBase = 'http://localhost:5000/';
    public static readonly UrlUser = UrlApi.UrlBase + 'api/user/';
    public static readonly UrlProduto = UrlApi.UrlBase + 'api/produto/';
    public static readonly UrlCategoria = UrlApi.UrlBase + 'api/categorias/';


    private static readonly UrlApiResources = UrlApi.UrlBase + 'resources/';
    public static readonly UrlImagesUsers = UrlApi.UrlApiResources + 'images/users/';
    public static readonly UrlImagesProdutos = UrlApi.UrlApiResources + 'images/produtos/';
    public static readonly UrlImagesCarousel = UrlApi.UrlApiResources + 'images/carousel/';


}
