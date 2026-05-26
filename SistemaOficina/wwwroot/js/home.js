document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("modalLogin");
    const btnAbrir = document.getElementById("btnAbrirLogin");
    const btnFechar = document.getElementById("btnFecharLogin");
    const formLogin = document.getElementById("formLogin");
    const erroLogin = document.getElementById("erroLogin");

    
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

    const slides = document.querySelectorAll(".carousel-slide");
    let slideAtual = 0;

    function proximoSlide() {
     
        slides[slideAtual].classList.remove("active");


        slideAtual = (slideAtual + 1) % slides.length;

        
        slides[slideAtual].classList.add("active");
    }

    setInterval(proximoSlide, 2000);
});