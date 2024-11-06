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
            <button type="button" class="custom-btn" @click="openLoginModal">
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
                <form @submit.prevent="authenticate('register')">
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
                <form @submit.prevent="authenticate('login')">
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
                    <button class="ghost" id="signIn">Entrar</button>
                  </div>
                  <div class="overlay-panel overlay-right">
                    <h1>Olá, Amigo!</h1>
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
    const loading = ref(false);
    let modal = null;

    const loginError = ref('');
    const registerError = ref('');
    const users = ref([]);

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

      fetchUsers();
    });

    const fetchUsers = async () => {
      try {
        const response = await api.get('/Usuario');
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

    const authenticate = async (type) => {
      const isLogin = type === 'login';
      const email = isLogin ? loginEmail.value : registerEmail.value;
      const password = isLogin ? loginPassword.value : registerPassword.value;

      // Limpa mensagens de erro
      if (isLogin) {
        loginError.value = '';
      } else {
        registerError.value = '';
      }
      loading.value = true;

      // Validações
      if (!validateEmail(email)) {
        if (isLogin) {
          loginError.value = 'Por favor, insira um email válido.';
        } else {
          registerError.value = 'Por favor, insira um email válido.';
        }
        loading.value = false;
        return;
      }

      if (!password || password.length < 6) {
        if (isLogin) {
          loginError.value = 'A senha deve ter pelo menos 6 caracteres.';
        } else {
          registerError.value = 'A senha deve ter pelo menos 6 caracteres.';
        }
        loading.value = false;
        return;
      }

      try {
        if (isLogin) {
          // Obtendo todos os usuários
          const response = await api.get('/Usuario');
          const usersList = response.data;

          // Verificando se as credenciais estão corretas
          const user = usersList.find(u => u.email === email && u.senha === password);
          
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            modal.hide();
            setTimeout(() => {
              isUserArea.value = true;
              router.push('/produtos');
            }, 100);
          } else {
            loginError.value = 'Usuário ou senha incorretos. Tente novamente.';
          }
        } else {
          // Registro
          const payload = { email, senha: password };
          const response = await api.post('/Usuario', payload);
          
          if (response.status === 201) {
            alert('Registro realizado com sucesso!');
            modal.hide();
            setTimeout(() => {
              isUserArea.value = true;
              router.push('/produtos');
            }, 100);
          } else {
            registerError.value = 'Erro ao registrar. Tente novamente.';
          }
        }
      } catch (error) {
        if (isLogin) {
          loginError.value = 'Erro ao fazer login. Tente novamente mais tarde.';
        } else {
          registerError.value = 'Erro ao registrar. Verifique sua conexão.';
        }
      } finally {
        loading.value = false;
      }
    };

    return {
      isUserArea,
      loginEmail,
      loginPassword,
      registerEmail,
      registerPassword,
      openLoginModal,
      authenticate,
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
