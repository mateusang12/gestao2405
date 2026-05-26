document.addEventListener("DOMContentLoaded", async () => {
    const selectMarca = document.getElementById("selectMarca");
    const selectModelo = document.getElementById("selectModelo");
    const selectLocalidade = document.getElementById("selectLocalidade"); // 📍 Mapeado para evitar erros
    const selectServico = document.getElementById("selectServico");
    const inputData = document.getElementById("dataAgendamento");
    const erroData = document.getElementById("erroData");

    // 1. Carrega todas as Marcas Reais da FIPE ao iniciar a página
    try {
        const res = await fetch('/api/agendamento/marcas');
        const marcas = await res.json();

        marcas.forEach(item => {
            const opt = document.createElement("option");
            opt.value = item.codigo; // Código usado para buscar os modelos depois (ex: "21")
            opt.textContent = item.nome; // Nome visível para o cliente (ex: "Fiat")
            selectMarca.appendChild(opt);
        });
    } catch (err) {
        console.error("Erro ao carregar marcas da FIPE:", err);
    }

    // 2. Escuta a mudança de marca para carregar os modelos em cascata
    // 2. Escuta a mudança de marca para carregar os modelos em cascata
    selectMarca.addEventListener("change", async (e) => {
        const codigoMarca = e.target.value; // Isso DEVE retornar o número (ex: "22")

        selectModelo.innerHTML = '<option value="">Selecione o Modelo</option>';
        if (!codigoMarca) {
            selectModelo.disabled = true;
            return;
        }

    // ... resto do fetch dos modelos continua igual ...

        try {
            // Busca os modelos associados àquela marca específica
            const res = await fetch(`/api/agendamento/marcas/${codigoMarca}/modelos`);
            const listaDeModelos = await res.json();

            // Como nosso backend agora já envia a lista pura, verificamos se ela é um Array válido
            if (Array.isArray(listaDeModelos) && listaDeModelos.length > 0) {
                listaDeModelos.forEach(item => {
                    const opt = document.createElement("option");
                    const nomeModelo = item.nome || item.Nome;

                    opt.value = nomeModelo;
                    opt.textContent = nomeModelo;
                    selectModelo.appendChild(opt);
                });

                // Desbloqueia o campo na tela para o cliente escolher
                selectModelo.disabled = false;
            } else {
                console.error("Não foi possível renderizar, o retorno não veio como lista:", listaDeModelos);
                selectModelo.disabled = true;
            }

        } catch (err) {
            console.error("Erro ao carregar modelos:", err);
            selectModelo.disabled = true;
        }
    });

    // 🛠️ 3. RECUPERADO: Carrega o Top 10 de Serviços vindo do Backend
    try {
        const resServicos = await fetch('/api/agendamento/servicos-oficina');
        const servicos = await resServicos.json();

        servicos.forEach(servico => {
            const opt = document.createElement("option");
            opt.value = servico;
            opt.textContent = servico;
            selectServico.appendChild(opt);
        });
    } catch (err) {
        console.error("Erro ao carregar serviços:", err);
    }

    // Configura trava de data mínima
    const agora = new Date();
    const ano = agora.getFullYear();
    const mes = String(agora.getMonth() + 1).padStart(2, '0');
    const dia = String(agora.getDate()).padStart(2, '0');
    const horas = String(agora.getHours()).padStart(2, '0');
    const minutos = String(agora.getMinutes()).padStart(2, '0');
    inputData.min = `${ano}-${mes}-${dia}T${horas}:${minutos}`;

    // Envio do formulário
    document.getElementById("formOficina").addEventListener("submit", async (e) => {
        e.preventDefault();
        erroData.classList.add("hidden");

        const dataSelecionada = new Date(inputData.value);
        if (dataSelecionada.getDay() === 0) {
            erroData.textContent = "A oficina não funciona aos domingos. Por favor, escolha de segunda a sábado.";
            erroData.classList.remove("hidden");
            return;
        }
        if (dataSelecionada.getHours() < 7 || dataSelecionada.getHours() >= 17) {
            erroData.textContent = "Horário inválido. Atendemos apenas das 07:00 às 17:00.";
            erroData.classList.remove("hidden");
            return;
        }

        const payload = {
            nomeCliente: document.getElementById("nomeCliente").value,
            telefone: document.getElementById("telefone").value,
            marcaCarro: selectMarca.options[selectMarca.selectedIndex].text,
            modeloCarro: selectModelo.value,
            localidade: selectLocalidade.value, // Usando a constante mapeada no topo
            tipoServico: selectServico.value,
            dataAgendamento: inputData.value
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
                selectModelo.disabled = true;
                inputData.min = `${ano}-${mes}-${dia}T${horas}:${minutos}`;
            } else {
                alert("Erro ao enviar dados. Verifique os campos.");
            }
        } catch (error) {
            console.error("Erro na requisição:", error);
        }
    });
});