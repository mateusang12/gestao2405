document.addEventListener("DOMContentLoaded", () => {
    
    const ordensServico = [
        { id: 101, cliente: "José Edson Anjos", veiculo: "Fiat Toro", servico: "Troca de Amortecedores e Suspensão", status: "andamento" },
        { id: 102, cliente: "Carlos Souza", veiculo: "Hyundai HB20", servico: "Troca de Óleo e Filtro", status: "concluido" }
    ];

    const listaAndamento = document.getElementById("listaAndamento");
    const listaConcluidos = document.getElementById("listaConcluidos");

    ordensServico.forEach(os => {
        const card = document.createElement("div");
        card.classList.add("card-os");

        card.innerHTML = `
            <h4>O.S. #${os.id} - ${os.cliente}</h4>
            <p class="veiculo">${os.veiculo}</p>
            <p><strong>Serviço:</strong> ${os.servico}</p>
        `;

        if (os.status === "andamento") {
            listaAndamento.appendChild(card);
        } else if (os.status === "concluido") {
            listaConcluidos.appendChild(card);
        }
    });
});