let cacheCarros = [];

document.addEventListener("DOMContentLoaded", async () => {
    // 1. Carregar Marcas e Modelos
    try {
        const resCarros = await fetch('/api/agendamento/carros-brasil');
        cacheCarros = await resCarros.json();

        const selectMarca = document.getElementById("selectMarca");
        cacheCarros.forEach(item => {
            const opt = document.createElement("option");
            opt.value = item.marca;
            opt.textContent = item.marca;
            selectMarca.appendChild(opt);
        });
    } catch (err) {
        console.error("Erro ao carregar os carros do Brasil:", err);
    }

    // 2. Carregar os 10 serviços mais procurados
    try {
        const resServicos = await fetch('/api/agendamento/servicos-oficina');
        const servicos = await resServicos.json();

        const selectServico = document.getElementById("selectServico");
        servicos.forEach(servico => {
            const opt = document.createElement("option");
            opt.value = servico;
            opt.textContent = servico;
            selectServico.appendChild(opt);
        });
    } catch (err) {
        console.error("Erro ao carregar serviços:", err);
    }

    // Lógica cascata: Atualizar modelos ao mudar a marca
    document.getElementById("selectMarca").addEventListener("change", (e) => {
        const marcaSelecionada = e.target.value;
        const selectModelo = document.getElementById("selectModelo");

        selectModelo.innerHTML = '<option value="">Selecione o Modelo</option>';

        if (!marcaSelecionada) {
            selectModelo.disabled = true;
            return;
        }

        const correspondente = cacheCarros.find(c => c.marca === marcaSelecionada);
        if (correspondente) {
            correspondente.modelos.forEach(modelo => {
                const opt = document.createElement("option");
                opt.value = modelo;
                opt.textContent = modelo;
                selectModelo.appendChild(opt);
            });
            selectModelo.disabled = false;
        }
    });

    // Envio do formulário (POST)
    document.getElementById("formOficina").addEventListener("submit", async (e) => {
        e.preventDefault();

        const payload = {
            nomeCliente: document.getElementById("nomeCliente").value,
            telefone: document.getElementById("telefone").value,
            marcaCarro: document.getElementById("selectMarca").value,
            modeloCarro: document.getElementById("selectModelo").value,
            tipoServico: document.getElementById("selectServico").value
        };

        try {
            const response = await fetch('/api/agendamento', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                const resultado = await response.json();
                const boxMsg = document.getElementById("mensagemSucesso");
                boxMsg.textContent = resultado.mensagem;
                boxMsg.classList.remove("hidden");
                document.getElementById("formOficina").reset();
                document.getElementById("selectModelo").disabled = true;
            } else {
                alert("Erro ao enviar dados. Verifique os campos.");
            }
        } catch (error) {
            console.error("Erro na requisição:", error);
        }
    });
});