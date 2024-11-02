import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7296", // Ajuste aqui se necessário
});

// Você pode adicionar interceptores ou configurações adicionais aqui, se precisar.

export default api;
