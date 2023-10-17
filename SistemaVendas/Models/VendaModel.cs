using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaVendas.Repositorys;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        private readonly IVendaRepository _repository;
        private readonly IProdutoRepository _produtoRepository;

        public VendaModel()
        {
                
        }
        public VendaModel(IVendaRepository repository, IProdutoRepository produtoRepository)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
        }

        [Key()]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Data")]
        public DateTime Data { get; set; }

        [Required]
        [Column("Total")]
        public decimal Total { get; set; }

        [Required]
        [Column("Quantidade_Produto")]
        public int QuantidadeProduto { get; set; }


        [ForeignKey("Cliente")]
        [Column("IdCliente")]
        public int IdCliente { get; set; }
        public virtual ClienteModel Cliente { get; set; }

        [ForeignKey("Produto")]
        [Column("IdProduto")]
        public int IdProduto { get; set; }
        public ProdutoModel Produto { get; set; }

        [ForeignKey("Vendedor")]
        [Column("IdVendedor")]
        public int IdVendedor { get; set; }
        public virtual VendedorModel Vendedor { get; set; }

        public async Task<bool> ExcluiVendaEAlteraQuantidadeProduto(VendaModel venda)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoPorId(venda.IdProduto);
                if (produto != null)
                {
                    produto.QuantidadeEstoque += venda.QuantidadeProduto;
                    _produtoRepository.Update(produto);
                    if (await _produtoRepository.SaveChanges())
                    {
                        _repository.Delete(venda);
                        if (await _repository.SaveChanges())
                            return true;
                    }
                }
            }
            catch
            {
            }            
            return false;
        }

        public async Task<bool> IncluiVendaEAlteraQuantidadeProduto(VendaModel venda, int idUserLogado)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoPorId(venda.IdProduto);
                if (produto != null)
                {
                    if (produto.QuantidadeEstoque - venda.QuantidadeProduto >= 0)
                    {
                        produto.QuantidadeEstoque -= venda.QuantidadeProduto;
                        _produtoRepository.Update(produto);
                        if (await _produtoRepository.SaveChanges())
                        {
                            venda.Data = DateTime.Now;
                            venda.Total = venda.QuantidadeProduto * produto.PrecoUnitario;
                            venda.IdVendedor = idUserLogado;
                            _repository.Add(venda);
                            if (await _repository.SaveChanges())
                                return true;
                        }
                    }
                }
            }
            catch
            {                
            }            
            return false;
        }
    }
}
