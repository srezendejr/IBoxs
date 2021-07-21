namespace IBoxs.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCatalog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        IdPessoa = c.Int(nullable: false, identity: true),
                        NomePessoa = c.String(maxLength: 1000, unicode: false),
                        Logradouro = c.String(maxLength: 1000, unicode: false),
                        Numero = c.String(maxLength: 1000, unicode: false),
                        CEP = c.String(maxLength: 1000, unicode: false),
                        Bairro = c.String(maxLength: 1000, unicode: false),
                        Cidade = c.String(maxLength: 1000, unicode: false),
                        Estado = c.String(maxLength: 1000, unicode: false),
                        Complemento = c.String(maxLength: 1000, unicode: false),
                        TipoPessoa = c.Int(nullable: false),
                        TipoDocumento = c.Int(nullable: false),
                        CNPJCPF = c.String(maxLength: 1000, unicode: false),
                        IERG = c.String(maxLength: 1000, unicode: false),
                        Email = c.String(maxLength: 1000, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdPessoa);
            
            CreateTable(
                "dbo.CarrinhoCompra",
                c => new
                    {
                        Indice = c.Long(nullable: false, identity: true),
                        Id = c.String(maxLength: 1000, unicode: false),
                        DataOperacao = c.DateTime(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdProdutoGrade = c.Int(nullable: false),
                        ReferenciaProduto = c.String(maxLength: 1000, unicode: false),
                        NomeProduto = c.String(maxLength: 1000, unicode: false),
                        ValorProduto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantidadeProduto = c.Int(nullable: false),
                        TamanhoSelecionado = c.String(maxLength: 1000, unicode: false),
                        CorSelecionada = c.String(maxLength: 1000, unicode: false),
                        Aberto = c.Int(nullable: false),
                        IdLoja = c.Int(nullable: false),
                        TokenProdutos = c.String(maxLength: 1000, unicode: false),
                        NCM = c.String(maxLength: 1000, unicode: false),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Indice)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .ForeignKey("dbo.Loja", t => t.IdLoja)
                .ForeignKey("dbo.ProdutoGrade", t => t.IdProdutoGrade)
                .Index(t => t.IdCliente)
                .Index(t => t.IdLoja)
                .Index(t => t.IdProdutoGrade);
            
            CreateTable(
                "dbo.ProdutoGrade",
                c => new
                    {
                        IdProdutoGrade = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        NomeCor = c.String(maxLength: 1000, unicode: false),
                        NomeTamanho = c.String(maxLength: 1000, unicode: false),
                        QtdEstoque = c.String(maxLength: 1000, unicode: false),
                        IdCor = c.Int(nullable: false),
                        IdTamanho = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProdutoGrade)
                .ForeignKey("dbo.Cor", t => t.IdCor)
                .ForeignKey("dbo.Produto", t => t.IdProduto)
                .ForeignKey("dbo.Tamanho", t => t.IdTamanho)
                .Index(t => t.IdCor)
                .Index(t => t.IdProduto)
                .Index(t => t.IdTamanho);
            
            CreateTable(
                "dbo.Cor",
                c => new
                    {
                        IdCor = c.Int(nullable: false, identity: true),
                        ReferenciaCor = c.String(maxLength: 1000, unicode: false),
                        NomeCor = c.String(maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => t.IdCor);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        IdProduto = c.Int(nullable: false, identity: true),
                        CodigoReferencia = c.String(maxLength: 1000, unicode: false),
                        NomeProduto = c.String(maxLength: 1000, unicode: false),
                        DescricaoProduto = c.String(maxLength: 1000, unicode: false),
                        ValorVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TamanhoUnico = c.Boolean(nullable: false),
                        CorUnica = c.Boolean(nullable: false),
                        IdLoja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProduto)
                .ForeignKey("dbo.Loja", t => t.IdLoja)
                .Index(t => t.IdLoja);
            
            CreateTable(
                "dbo.Tamanho",
                c => new
                    {
                        IdTamanho = c.Int(nullable: false, identity: true),
                        ReferenciaTamanho = c.String(maxLength: 1000, unicode: false),
                        NomeTamanho = c.String(maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => t.IdTamanho);
            
            CreateTable(
                "dbo.ProdutoFoto",
                c => new
                    {
                        IdProdutoFoto = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        Foto = c.String(maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => t.IdProdutoFoto)
                .ForeignKey("dbo.Produto", t => t.IdProduto)
                .Index(t => t.IdProduto);
            
            CreateTable(
                "dbo.Campanha",
                c => new
                    {
                        IdCampanha = c.Int(nullable: false, identity: true),
                        NomeCampnha = c.String(maxLength: 1000, unicode: false),
                        TokenCampanha = c.String(maxLength: 1000, unicode: false),
                        DataInicial = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdCampanha);
            
            CreateTable(
                "dbo.CampanhaProduto",
                c => new
                    {
                        IdCampanhaProduto = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        IdCampanha = c.Int(nullable: false),
                        PercDesconto = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdCampanhaProduto)
                .ForeignKey("dbo.Campanha", t => t.IdCampanha)
                .ForeignKey("dbo.Produto", t => t.IdProduto)
                .Index(t => t.IdCampanha)
                .Index(t => t.IdProduto);
            
            CreateTable(
                "dbo.Loja",
                c => new
                    {
                        IdPessoa = c.Int(nullable: false),
                        Id = c.String(maxLength: 1000, unicode: false),
                        RazaoSocial = c.String(maxLength: 100, unicode: false),
                        IntegradorPagamento = c.Int(nullable: false),
                        PublicKey = c.String(maxLength: 1000, unicode: false),
                        AccessToken = c.String(maxLength: 1000, unicode: false),
                        TaxaBoleto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Whatsapp = c.String(maxLength: 1000, unicode: false),
                        LogoMarca = c.String(maxLength: 1000, unicode: false),
                        QuemSomos = c.String(maxLength: 1000, unicode: false),
                        PoliticaPrivacidade = c.String(maxLength: 1000, unicode: false),
                        PoliticaTrocaDevolucao = c.String(maxLength: 1000, unicode: false),
                        VerEstoque = c.Boolean(nullable: false),
                        PagamentoRetira = c.Boolean(nullable: false),
                        QtdMinima = c.Int(nullable: false),
                        IdFacebookPixel = c.String(maxLength: 1000, unicode: false),
                        MensagemCompartilhamento = c.String(maxLength: 20, unicode: false),
                        Senha = c.String(maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => t.IdPessoa)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        IdPessoa = c.Int(nullable: false),
                        BloqueadaCompra = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdPessoa)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Loja", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.CampanhaProduto", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.CampanhaProduto", "IdCampanha", "dbo.Campanha");
            DropForeignKey("dbo.ProdutoFoto", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.CarrinhoCompra", "IdProdutoGrade", "dbo.ProdutoGrade");
            DropForeignKey("dbo.ProdutoGrade", "IdTamanho", "dbo.Tamanho");
            DropForeignKey("dbo.ProdutoGrade", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.Produto", "IdLoja", "dbo.Loja");
            DropForeignKey("dbo.ProdutoGrade", "IdCor", "dbo.Cor");
            DropForeignKey("dbo.CarrinhoCompra", "IdLoja", "dbo.Loja");
            DropForeignKey("dbo.CarrinhoCompra", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.Cliente", new[] { "IdPessoa" });
            DropIndex("dbo.Loja", new[] { "IdPessoa" });
            DropIndex("dbo.CampanhaProduto", new[] { "IdProduto" });
            DropIndex("dbo.CampanhaProduto", new[] { "IdCampanha" });
            DropIndex("dbo.ProdutoFoto", new[] { "IdProduto" });
            DropIndex("dbo.CarrinhoCompra", new[] { "IdProdutoGrade" });
            DropIndex("dbo.ProdutoGrade", new[] { "IdTamanho" });
            DropIndex("dbo.ProdutoGrade", new[] { "IdProduto" });
            DropIndex("dbo.Produto", new[] { "IdLoja" });
            DropIndex("dbo.ProdutoGrade", new[] { "IdCor" });
            DropIndex("dbo.CarrinhoCompra", new[] { "IdLoja" });
            DropIndex("dbo.CarrinhoCompra", new[] { "IdCliente" });
            DropTable("dbo.Cliente");
            DropTable("dbo.Loja");
            DropTable("dbo.CampanhaProduto");
            DropTable("dbo.Campanha");
            DropTable("dbo.ProdutoFoto");
            DropTable("dbo.Tamanho");
            DropTable("dbo.Produto");
            DropTable("dbo.Cor");
            DropTable("dbo.ProdutoGrade");
            DropTable("dbo.CarrinhoCompra");
            DropTable("dbo.Pessoa");
        }
    }
}
