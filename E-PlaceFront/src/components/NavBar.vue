<template>
  <div>
    <nav class="navbar navbar-expand-lg fixed-top">
      <div class="container-fluid">
        <div class="navbar-brand">
          <h1>
            <a href="/" class="navbar-brand">
              <img id="logoNav" src="../assets/img/logo.png" alt="E-Place">
              <strong class="logo-text">E-place</strong>
            </a>
          </h1>
        </div>
        <div class="navbar-collapse justify-content-end">
          <template v-if="!isUserArea">
            <button type="button" class=" custom-btn" @click="openLoginModal">
              <strong>Login</strong>
            </button>
          </template>
        </div>
      </div>
    </nav>

    <!-- Modal de login -->
    <div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true" ref="loginModal">
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <div class="modal-body">
            <div class="container" id="container">
              <div class="form-container sign-up-container">
                <form @submit.prevent="register">
                  <h1 class="login">Criar Conta</h1>
                  <div class="social-container">
                    <a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
                    <a href="#" class="social"><i class="fab fa-google-plus-g"></i></a>
                    <a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
                  </div>
                  <span>ou use seu email para registrar</span>
                  <input type="email" placeholder="Email" v-model="registerEmail" required />
                  <input type="password" placeholder="Senha" v-model="registerPassword" required />
                  <p v-if="registerError" class="error-message">{{ registerError }}</p>
                  <button type="submit" :disabled="loading">{{ loading ? 'Carregando...' : 'Registrar' }}</button>
                </form>
              </div>
              <div class="form-container sign-in-container">
                <form @submit.prevent="login">
                  <h1 class="login">Entrar</h1>
                  <div class="social-container">
                    <a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
                    <a href="#" class="social"><i class="fab fa-google-plus-g"></i></a>
                    <a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
                  </div>
                  <span>ou use sua conta</span>
                  <input type="email" placeholder="Email" v-model="loginEmail" required />
                  <input type="password" placeholder="Senha" v-model="loginPassword" required />
                  <a href="#">Esqueceu sua senha?</a>
                  <p v-if="loginError" class="error-message">{{ loginError }}</p>
                  <button type="submit" :disabled="loading">{{ loading ? 'Carregando...' : 'Entrar' }}</button>
                </form>
              </div>
              <div class="overlay-container">
                <div class="overlay">
                  <div class="overlay-panel overlay-left">
                    <h1>Bem-vindo de Volta!</h1>
                    <p></p>
                    <button class="ghost" id="signIn">Entrar</button>
                  </div>
                  <div class="overlay-panel overlay-right">
                    <h1>Olá, Amigo!</h1>
                    <p></p>
                    <button class="ghost" id="signUp">Registrar</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
import api from '../router/api';
import "../assets/img/css/NavBar.css";

export default {
  setup() {
    const router = useRouter();
    const isUserArea = ref(false);
    const loginEmail = ref('');
    const loginPassword = ref('');
    const registerEmail = ref('');
    const registerPassword = ref('');
    const loginModal = ref(null);
    const loading = ref(false); // Indicador de carregamento
    let modal = null;

    const loginError = ref('');
    const registerError = ref('');
    const users = ref([]); // Lista de usuários registrados

    // Usuário administrador pré-cadastrado
    const adminUser = { email: 'admin@eplace.com', password: 'admin123' };

    onMounted(() => {
      if (loginModal.value) {
        modal = new Modal(loginModal.value);
      }

      const signUpButton = document.getElementById('signUp');
      const signInButton = document.getElementById('signIn');
      const container = document.getElementById('container');

      if (signUpButton && signInButton && container) {
        signUpButton.addEventListener('click', () => {
          container.classList.add("right-panel-active");
        });

        signInButton.addEventListener('click', () => {
          container.classList.remove("right-panel-active");
        });
      }

      fetchUsers(); // Buscar usuários ao montar o componente
    });

    const fetchUsers = async () => {
      try {
        const response = await api.get('/Usuario'); // GET para listar usuários
        users.value = response.data;
      } catch (error) {
        console.error('Erro ao buscar usuários:', error);
      }
    };

    const openLoginModal = () => {
      if (!isUserArea.value && modal) {
        modal.show();
      }
    };

    const validateEmail = (email) => {
      const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return re.test(String(email).toLowerCase());
    };

    const login = async () => {
  loginError.value = '';
  loading.value = true;

  if (!validateEmail(loginEmail.value)) {
    loginError.value = 'Por favor, insira um email válido.';
    loading.value = false;
    return;
  }

  try {
    // Chamada à nova rota de login no backend
    const response = await api.post('/Usuario/login', {
      email: loginEmail.value,
      senha: loginPassword.value
    });

    if (response.status === 200) {
      localStorage.setItem('user', JSON.stringify(response.data));
      modal.hide(); 
      setTimeout(() => {
        isUserArea.value = true;
        router.push('/produtos');
      }, 100);
    } else {
      loginError.value = 'Usuário ou senha incorretos. Tente novamente.';
    }
  } catch (error) {
    loginError.value = 'Erro ao fazer login. Tente novamente mais tarde.';
  } finally {
    loading.value = false;
  }
};

const register = async () => {
  registerError.value = '';
  loading.value = true;

  if (!validateEmail(registerEmail.value)) {
    registerError.value = 'Por favor, insira um email válido.';
    loading.value = false;
    return;
  }

  if (!registerPassword.value || registerPassword.value.length < 6) {
    registerError.value = 'A senha deve ter pelo menos 6 caracteres.';
    loading.value = false;
    return;
  }

  try {
    const response = await api.post('/Usuario', {
      email: registerEmail.value,
      senha: registerPassword.value,
    });

    if (response.status === 201) {
      alert('Registro realizado com sucesso!');
      modal.hide(); 
      setTimeout(() => {
        isUserArea.value = true;
        router.push('/produtos'); // Redireciona para a página de cadastro de produtos
      }, 100);
    } else {
      registerError.value = 'Erro ao registrar. Tente novamente.';
    }
  } catch (error) {
    registerError.value = 'Erro ao registrar. Verifique sua conexão.';
  } finally {
    loading.value = false;
  }
};


    const navigateToProducts = () => {
      router.push('/produtos');
    };

    return {
      isUserArea,
      loginEmail,
      loginPassword,
      registerEmail,
      registerPassword,
      openLoginModal,
      login,
      register,
      loginModal,
      loginError,
      registerError,
      users,
      loading,
    };
  }
};
</script>




<style scoped>
@import url('https://fonts.googleapis.com/css?family=Montserrat:400,800');
</style>