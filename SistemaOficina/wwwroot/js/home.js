document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("modalLogin");
    const btnAbrir = document.getElementById("btnAbrirLogin");
    const btnFechar = document.getElementById("btnFecharLogin");
    const formLogin = document.getElementById("formLogin");
    const erroLogin = document.getElementById("erroLogin");

    // Abrir e fechar modal
    btnAbrir.addEventListener("click", () => modal.classList.remove("hidden"));
    btnFechar.addEventListener("click", () => {
        modal.classList.add("hidden");
        erroLogin.classList.add("hidden");
    });

    // Validação estática de Login (Simulação do Admin)
    formLogin.addEventListener("submit", (e) => {
        e.preventDefault();
        const usuario = document.getElementById("usuario").value;
        const senha = document.getElementById("senha").value;

        if (usuario === "admin" && senha === "1234") {
            window.location.href = "dashboard.html"; // Redireciona para o painel de ordens
        } else {
            erroLogin.classList.remove("hidden");
        }
    });
});