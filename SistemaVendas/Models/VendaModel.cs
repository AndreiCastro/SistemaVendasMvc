using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaVendas.Repository;
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
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [DisplayName("Quantidade do Produto")]
        public int Quantidade_Produto { get; set; }


        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public virtual ClienteModel Cliente { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        public ProdutoModel Produto { get; set; }

        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public virtual VendedorModel Vendedor { get; set; }

        public async Task<bool> ExcluiVendaEAlteraQuantidadeProduto(VendaModel venda)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoPorId(venda.IdProduto);
                if (produto != null)
                {
                    produto.QuantidadeEstoque += venda.Quantidade_Produto;
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
                    if (produto.QuantidadeEstoque - venda.Quantidade_Produto >= 0)
                    {
                        produto.QuantidadeEstoque -= venda.Quantidade_Produto;
                        _produtoRepository.Update(produto);
                        if (await _produtoRepository.SaveChanges())
                        {
                            venda.Data = DateTime.Now;
                            venda.Total = venda.Quantidade_Produto * produto.PrecoUnitario;
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
