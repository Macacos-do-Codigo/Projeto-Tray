import axios from "axios";

// Cria a instância do Axios com a URL base da API
const api = axios.create({
  baseURL: "https://localhost:7296", // Ajuste aqui se necessário
});

// Interceptor para adicionar o token JWT ao cabeçalho Authorization
api.interceptors.request.use(
  (config) => {
    // Recupera o token do localStorage (ou sessionStorage) caso esteja presente
    const token = localStorage.getItem("token"); // Altere para sessionStorage se necessário
    
    // Se o token existir, adiciona ao cabeçalho Authorization
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api;
