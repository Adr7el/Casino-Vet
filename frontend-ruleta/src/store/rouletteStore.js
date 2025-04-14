import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getUsuarioPorNombre, apostarBackend, guardarUsuario } from '@/services/rouletteService'

export const rouletteStore = defineStore('roulette', () => {

    const isSpinning = ref(false)

    const currentBet = ref(null)

    const usuario = ref(null)
    const errorUsuario = ref(null)

    const resultadoApuesta = ref(null)
    const mensajeErrorApuesta = ref(null)

    // Acciones
    function startSpin() {
        isSpinning.value = true
    }

    function stopSpin() {
        isSpinning.value = false
    }

    function setBet(bet) {
        currentBet.value = bet
    }

    function clearBet() {
        currentBet.value = null
    }

    function clearResultadoApuesta() {
        resultadoApuesta.value = null
        mensajeErrorApuesta.value = null
    }    

    async function buscarUsuario(nombre) {
        try {
            const data = await getUsuarioPorNombre(nombre)
            usuario.value = data
            errorUsuario.value = null
        } catch (error) {
            usuario.value = null
            errorUsuario.value = error
        }
    }

    async function apostar(resultadoRuleta) {
        if (!usuario.value || !currentBet.value || !resultadoRuleta) return

        isSpinning.value = true
        resultadoApuesta.value = null
        mensajeErrorApuesta.value = null

        try {
            const numeroObtenido = resultadoRuleta.id
            const colorObtenido = resultadoRuleta.background

            const payload = {
                nombreUsuario: usuario.value.nombre,
                monto: currentBet.value.monto,
                numeroObtenido,
                colorObtenido
            }

            // Asignar campos por tipo de apuesta
            switch (currentBet.value.tipo) {
                case 'numero':
                    payload.numero = currentBet.value.valor
                    payload.color = currentBet.value.valor === 0 ? 'green' : colorObtenido
                    break
                case 'color':
                    payload.color = currentBet.value.valor 
                    break
                case 'paridad_color':
                    payload.esPar = currentBet.value.valor.paridad === 'par'
                    payload.color = currentBet.value.valor.color 
                    break
            }

            const resultado = await apostarBackend(payload)

            if (resultado.gano) {
                usuario.value.saldo += resultado.nuevoSaldo
            } else {
                usuario.value.saldo -= currentBet.value.monto
            }

            resultadoApuesta.value = resultado

        } catch (error) {
            mensajeErrorApuesta.value = error?.response?.data?.message || 'Error al apostar'
        } finally {
            isSpinning.value = false
        }
    }

    async function guardarSaldoUsuario() {
        if (!usuario.value) return

        const payload = {
            nombre: usuario.value.nombre,
            saldo: usuario.value.saldo
        }
    
        try {
            const mensaje = await guardarUsuario(payload)
            console.log('Usuario guardado correctamente:', mensaje)
        } catch (error) {
            console.error('Error al guardar usuario:', error)
        }
    }

    return {
        isSpinning,
        currentBet,
        usuario,
        errorUsuario,
        resultadoApuesta,
        mensajeErrorApuesta,
        startSpin,
        stopSpin,
        setBet,
        clearBet,
        clearResultadoApuesta,
        buscarUsuario,
        apostar,
        guardarSaldoUsuario
    }
})
