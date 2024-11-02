<template>
  <div>
    <div class="product-list">
      <div class="table-container">
        <div class="header-container">
          <h2 class="titulo-historico">Histórico de Produtos</h2>
          <div class="actions">
            <button class="btn-primary" @click="abrirModalCadastro">Cadastrar Produtos</button>
          </div>
        </div>
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Nome</th>
              <th>Marca</th>
              <th>Preço</th>
              <th>Ecommerce</th>
              <th>Qtde. Estoque</th>
              <th>Data de Cadastro</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(product, index) in paginatedProducts" :key="product.id">
              <td>{{ product.id }}</td>
              <td>{{ product.modelo }}</td>
              <td>{{ product.marca }}</td>
              <td v-if="product.preço">R$ {{ product.preço.toFixed(2) }}</td>
              <td v-else></td>
              <td>{{ formatarEcommerce(product) }}</td>
              <td>{{ product.quantidadeEmEstoque }}</td>
              <td>{{ formatarData(product.dataCadastro) }}</td>
              <td>
                <i @click="selecionarProdutoParaExcluir(index)" class="bi bi-trash"></i>
              </td>
            </tr>
            <tr v-if="products.length === 0">
              <td colspan="8" class="no-products-message"><b>Nenhum produto cadastrado.</b></td>
            </tr>
          </tbody>
        </table>
        <div class="pagination">
          <button @click="previousPage" :disabled="currentPage === 1">Anterior</button>
          <span v-for="page in totalPages" :key="page">
            <button @click="currentPage = page" :disabled="currentPage === page">{{ page }}</button>
          </span>
          <button @click="nextPage" :disabled="currentPage === totalPages">Próximo</button>
        </div>
      </div>
    </div>

    <!-- Modal de Cadastro -->
    <div class="modal fade" id="escolhaEcommerceModal" tabindex="-1" aria-labelledby="escolhaEcommerceModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-custom-color">
        <div class="modal-content">
          <div class="modal-body">
            <!-- Formulário de cadastro -->
            <div class="form-group">
              <label for="modelo">Nome do Produto:</label>
              <input type="text" id="modelo" v-model="produto.modelo" class="form-control" placeholder="Nome do produto">
            </div>
            <div class="form-group">
              <label for="marca">Marca:</label>
              <input type="text" id="marca" v-model="produto.marca" class="form-control" placeholder="Marca do produto">
            </div>
            <div class="form-group">
              <label for="preço">Preço:</label>
              <input type="text" id="preço" v-model="produto.preço" class="form-control" placeholder="1.000,00" @input="mascararValor" @blur="formatarValor" />
            </div>
            <div class="form-group">
              <label for="quantidadeEmEstoque">Quantidade em Estoque:</label>
              <input type="number" id="quantidadeEmEstoque" v-model="produto.quantidadeEmEstoque" class="form-control" placeholder="Quantidade disponível">
            </div>
            <div class="form-group ecommerce-flags">
              <label>Ecommerce:</label>
              <div class="form-check">
                <input class="form-check-input" type="checkbox" id="mercadoLivre" v-model="produto.mercadoLivre">
                <label class="form-check-label" for="mercadoLivre">Mercado Livre</label>
              </div>
              <div class="form-check">
                <input class="form-check-input" type="checkbox" id="aliExpress" v-model="produto.aliExpress">
                <label class="form-check-label" for="aliExpress">AliExpress</label>
              </div>
              <div class="form-check">
                <input class="form-check-input" type="checkbox" id="shopee" v-model="produto.shopee">
                <label class="form-check-label" for="shopee">Shopee</label>
              </div>
            </div>
          </div>
          <div class="modal-footer justify-content-center">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
            <button type="button" class="btn btn-primary" @click="salvarProduto">Salvar Produto</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import api from "../router/api";
import "../assets/img/css/ProductList.css";

export default {
  name: 'ProductList',
  data() {
    return {
      products: [],
      produto: {
        modelo: '',
        marca: '',
        preço: 0,
        quantidadeEmEstoque: 0,
        mercadoLivre: false,
        aliExpress: false,
        shopee: false,
        dataCadastro: ''
      },
      produtoParaExcluir: null,
      error: '',
      showModal: false,
      showDeleteModal: false,
      currentPage: 1,
      itemsPerPage: 6
    };
  },
  created() {
    this.getProducts();
  },
  computed: {
    totalPages() {
      return Math.ceil(this.products.length / this.itemsPerPage);
    },
    paginatedProducts() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      return this.products.slice(start, start + this.itemsPerPage);
    }
  },
  methods: {
    abrirModalCadastro() {
      this.produto = {
        modelo: '',
        marca: '',
        preço: 0,
        quantidadeEmEstoque: 0,
        mercadoLivre: false,
        aliExpress: false,
        shopee: false
      };
      this.showModal = true;
      this.$nextTick(() => {
        const modalElement = new bootstrap.Modal(document.getElementById('escolhaEcommerceModal'));
        modalElement.show();
      });
    },
    async getProducts() {
      try {
        const response = await api.get("/Produtos");
        this.products = response.data;
      } catch (error) {
        console.error("Erro ao obter produtos:", error);
        this.products = [];
      }
    },
    formatarData(data) {
      const date = new Date(data);
      return date.toLocaleDateString('pt-BR');
    },
    formatarEcommerce(product) {
      const ecommerces = [];
      if (product.mercadoLivre) ecommerces.push('Mercado Livre');
      if (product.aliExpress) ecommerces.push('AliExpress');
      if (product.shopee) ecommerces.push('Shopee');
      return ecommerces.join(', ');
    },
    mascararValor(event) {
      let valor = event.target.value;
      valor = valor.replace(/\D/g, ""); // Remove caracteres não numéricos
      valor = (valor / 100).toFixed(2).replace(".", ",");
      valor = valor.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      event.target.value = valor;
      this.produto.preço = valor;
    },
    formatarValor(event) {
      let valor = event.target.value;
      if (!valor.includes(",")) {
        valor += ",00"; // Adiciona centavos se estiverem faltando
      }
      event.target.value = valor;
      this.produto.preço = parseFloat(valor.replace(".", "").replace(",", "."));
    },
    async salvarProduto() {
      try {
        const response = await api.post("/Produtos", this.produto);
        
        // Adiciona o novo produto à lista local de produtos
        this.products.push(response.data);
        
        // Fecha o modal de forma segura
        const modalElement = bootstrap.Modal.getInstance(document.getElementById('escolhaEcommerceModal'));
        if (modalElement) modalElement.hide();
        
        // Atualiza o estado para garantir fechamento do modal
        this.showModal = false;

        // Limpa o formulário
        this.produto = {
          modelo: '',
          marca: '',
          preço: 0,
          quantidadeEmEstoque: 0,
          mercadoLivre: false,
          aliExpress: false,
          shopee: false
        };
      } catch (error) {
        console.error("Erro ao salvar o produto:", error.response ? error.response.data : error);
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    previousPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    }
  }
};
</script>
