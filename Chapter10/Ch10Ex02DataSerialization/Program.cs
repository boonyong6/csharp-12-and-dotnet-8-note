using Ch10Ex02DataSerialization.AutoGenModels;

string outputDirectory = Path.Combine(Environment.CurrentDirectory, "output");
Directory.CreateDirectory(outputDirectory);

ICollection<Category> categories = QueryAllCategories();
ICollection<Product> products = QueryAllProducts();

SerializeToFiles(categories, outputDirectory);
SerializeToFiles(products, outputDirectory);
