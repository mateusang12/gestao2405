document.addEventListener("DOMContentLoaded", () => {
    // Dados mocados simulando ordens vindas do banco de dados/API
    const ordensServico = [
        { id: 101, cliente: "José Edson Anjos", veiculo: "Fiat Toro", servico: "Troca de Amortecedores e Suspensão", status: "andamento" },
        { id: 102, cliente: "Maria Silva", veiculo: "Chevrolet Onix", servico: "Alinhamento e Balanceamento de Rodas", status: "andamento" },
        { id: 103, cliente: "Carlos Souza", veiculo: "Hyundai HB20", servico: "Troca de Óleo e Filtro", status: "concluido" },
        { id: 104, cliente: "Ana Costa", veiculo: "Toyota Corolla", servico: "Diagnóstico de Injeção Eletrônica", status: "concluido" }
    ];

    const listaAndamento = document.getElementById("listaAndamento");
    const listaConcluidos = document.getElementById("listaConcluidos");

    // Renderizar os cards dinamicamente nas colunas corretas
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