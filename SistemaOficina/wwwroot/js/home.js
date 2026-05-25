document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("modalLogin");
    const btnAbrir = document.getElementById("btnAbrirLogin");
    const btnFechar = document.getElementById("btnFecharLogin");
    const formLogin = document.getElementById("formLogin");
    const erroLogin = document.getElementById("erroLogin");

    // Lógica do Modal de Login
    btnAbrir.addEventListener("click", () => modal.classList.remove("hidden"));
    btnFechar.addEventListener("click", () => {
        modal.classList.add("hidden");
        erroLogin.classList.add("hidden");
    });

    formLogin.addEventListener("submit", (e) => {
        e.preventDefault();
        const usuario = document.getElementById("usuario").value;
        const senha = document.getElementById("senha").value;

        if (usuario === "admin" && senha === "1234") {
            window.location.href = "dashboard.html";
        } else {
            erroLogin.classList.remove("hidden");
        }
    });

    // NOVO: Lógica de Automação do Carrossel (2 segundos)
    const slides = document.querySelectorAll(".carousel-slide");
    let slideAtual = 0;

    function proximoSlide() {
        // Remove a classe ativa do slide que está aparecendo agora
        slides[slideAtual].classList.remove("active");

        // Calcula o índice do próximo slide (volta para 0 ao chegar no fim)
        slideAtual = (slideAtual + 1) % slides.length;

        // Adiciona a classe ativa no novo slide
        slides[slideAtual].classList.add("active");
    }

    // Inicia a rotação automática a cada 2000ms (2 segundos)
    setInterval(proximoSlide, 2000);
});