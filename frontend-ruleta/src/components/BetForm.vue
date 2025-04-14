<template>
    <v-card class="pa-4 mx-auto" max-width="400" color="#282828" dark>
        <h2 class="mb-4">💸 Realiza tu Apuesta</h2>

        <v-form ref="formRef" @submit.prevent="submitBet">

            <v-select v-model="tipo" label="Tipo de apuesta" :items="tiposApuesta" item-title="title" item-value="value"
                dense outlined required :rules="[v => !!v || 'Campo requerido']" />

            <v-text-field v-if="tipo === 'numero'" v-model.number="numero" label="Número (0-36)" type="number" dense
                outlined :rules="[
                    v => v !== null || 'Campo requerido',
                    v => v >= 0 || 'Debe ser mayor o igual a 0',
                    v => v <= 36 || 'Debe ser menor o igual a 36'
                ]" />

            <v-select v-if="tipo === 'color'" v-model="color" label="Color" :items="colores" item-title="title"
                item-value="value" dense outlined :rules="[v => !!v || 'Campo requerido']" />

            <div v-if="tipo === 'paridad_color'">
                <v-select v-model="paridad" label="Paridad" :items="['par', 'impar']" dense outlined class="mb-2"
                    :rules="[v => !!v || 'Campo requerido']" />

                <v-select v-model="color" label="Color" :items="colores" item-title="title" item-value="value" dense
                    outlined :rules="[v => !!v || 'Campo requerido']" />
            </div>

            <v-text-field v-model.number="monto" label="Monto de la apuesta" type="number" min="1" dense outlined
                class="mt-2" :rules="[
                    v => !!v || 'Campo requerido',
                    v => v > 0 || 'Debe ser mayor a 0'
                ]" />

            <div v-if="usuario" class="text-caption text-grey-lighten-1 mb-2">
                💰 Saldo disponible: {{ usuario.saldo.toFixed(2) }}
            </div>

            <div v-if="error" class="text-red text-caption mb-3">
                {{ error }}
            </div>

            <v-row class="mt-2" justify="center" align="center">
                <v-col cols="6">
                    <v-btn type="submit" :disabled="isSpinning || !apuestaValida" color="blue" block>
                        Apostar
                    </v-btn>
                </v-col>
                <v-col cols="6">
                    <v-btn type="button" color="green" block @click="guardarPartida">
                        Guardar Partida
                    </v-btn>
                </v-col>
            </v-row>
        </v-form>
    </v-card>

    <v-snackbar v-model="snackbar" :color="snackbarColor" timeout="3000" location="top" class="text-center">
        {{ snackbarMsg }}
    </v-snackbar>

</template>

<script setup>
import { ref, computed, nextTick } from 'vue'
import { rouletteStore } from '@/store/rouletteStore'
import { storeToRefs } from 'pinia'

const props = defineProps({
    isSpinning: Boolean
})

const emit = defineEmits(['bet-submitted'])

const store = rouletteStore()
const { usuario } = storeToRefs(store)

const tipo = ref(null)
const numero = ref(null)
const color = ref(null)
const paridad = ref(null)
const monto = ref(null)
const error = ref('')
const formRef = ref(null)

const snackbar = ref(false)
const snackbarMsg = ref('')
const snackbarColor = ref('success')


const tiposApuesta = [
    { value: 'numero', title: 'Número específico' },
    { value: 'color', title: 'Color (rojo/negro)' },
    { value: 'paridad_color', title: 'Par/Impar + Color' }
]

const colores = [
    { value: 'red', title: 'Rojo' },
    { value: 'black', title: 'Negro' }
]

const apuestaValida = computed(() => {
    if (!tipo.value || !monto.value || monto.value <= 0) return false

    if (tipo.value === 'numero') {
        return numero.value !== null && numero.value >= 0 && numero.value <= 36
    }

    if (tipo.value === 'color') {
        return !!color.value
    }

    if (tipo.value === 'paridad_color') {
        return !!paridad.value && !!color.value
    }

    return false
})

const submitBet = () => {
    const valid = formRef.value?.validate()
    if (!valid) return

    error.value = ''

    if (!usuario.value) {
        error.value = 'Usuario no válido.'
        return
    }

    if (monto.value > usuario.value.saldo) {
        error.value = 'Saldo insuficiente.'
        return
    }

    store.clearResultadoApuesta()

    const bet = {
        tipo: tipo.value,
        monto: monto.value,
        valor: null
    }

    if (tipo.value === 'numero') {
        bet.valor = numero.value
    } else if (tipo.value === 'color') {
        bet.valor = color.value
    } else if (tipo.value === 'paridad_color') {
        bet.valor = {
            paridad: paridad.value,
            color: color.value
        }
    }

    emit('bet-submitted', bet)
    resetFields()
}

const resetFields = async () => {
    tipo.value = null
    numero.value = null
    color.value = null
    paridad.value = null
    monto.value = null
    error.value = ''

    await nextTick() // Para que Vue actualice el DOM/reactividad
    formRef.value?.resetValidation()
}

const guardarPartida = async () => {
    if (!usuario.value) {
        console.warn('No hay usuario cargado')
        return
    }

    try {
        await store.guardarSaldoUsuario()
        snackbarMsg.value = '¡Partida guardada con éxito!'
        snackbarColor.value = 'success'
    } catch (e) {
        snackbarMsg.value = 'Error al guardar la partida.'
        snackbarColor.value = 'error'
    } finally {
        snackbar.value = true
    }
}
</script>