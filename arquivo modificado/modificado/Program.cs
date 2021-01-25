using FluentAssertions;
using Xunit;
using Vaquinha.Tests.Common.Fixtures;

namespace Vaquinha.Unit.Tests.DomainTests
{
    [Collection(nameof(DoacaoFixtureCollection))]    
    public class DoacaoTests: IClassFixture<DoacaoFixture>, 
                              IClassFixture<EnderecoFixture>, 
                              IClassFixture<CartaoCreditoFixture>
    {
        private readonly DoacaoFixture _doacaoFixture;
        private readonly EnderecoFixture _enderecoFixture;
        private readonly CartaoCreditoFixture _cartaoCreditoFixture;

        public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }

        [Fact]
        [Trait("Doacao", "Doacao_UsuarioAceitaPagarComTaxa_DoacaoValida")]
        public void Doacao_UsuarioAceitaPagarComTaxa_DoacaoValida()
        {           
            // Arrange
            var doacao = _doacaoFixture.DoacaoValida(false, 5, false, true);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            doacao.Valor.Should().Be(6,because: "valor com taxa 20%");
            doacao.ErrorMessages.Should().BeEmpty();
        }

      
     

        

        

        

        
    }
}
