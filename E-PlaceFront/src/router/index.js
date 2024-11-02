import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import ProductList from '../components/ProductList.vue'; // Importe o componente ProductList
import { isAuthenticated } from './auth'; // Função de verificação de autenticação

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/produtos', // Adicione a rota /produtos
      name: 'ProductList',
      component: ProductList,
      meta: { requiresAuth: true } // Define como rota protegida (se necessário)
    },
  ]
});

// Adiciona uma guarda global para verificar autenticação
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!isAuthenticated()) {
      next('/'); // Redireciona para a página inicial se não estiver autenticado
    } else {
      next(); // Prossegue para a rota protegida
    }
  } else {
    next(); // Prossegue para a rota normalmente
  }
});

export default router;
