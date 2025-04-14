<template>
    <v-card class="pa-4 mx-auto" max-width="400" color="#1f1f1f" dark>
        <h2>👤 Ingresar Usuario</h2>

        <v-form @submit.prevent="handleSubmit">
            <v-text-field v-model="nombre" label="Nombre" dense outlined :rules="[v => !!v || 'El nombre es requerido']"
                @input="resetState" class="mb-2" />

            <div v-if="errorUsuario" class="text-red text-caption mb-4">
                {{ errorUsuario }}
            </div>

            <v-btn :disabled="!nombre.trim()" type="submit" color="green" class="mb-8" block>
                Buscar
            </v-btn>
        </v-form>

        <div v-if="usuario">
            <p>✅ Usuario encontrado: <strong>{{ usuario.nombre }}</strong></p>
            <p>💰 Saldo disponible: <strong>{{ usuario.saldo.toFixed(2) }}</strong></p>
            <v-btn @click="emitirUsuario" color="primary" block>Continuar</v-btn>
        </div>

        <div v-else-if="buscado && !usuario">
            <p>⚠️ Usuario no encontrado. Ingrese el saldo inicial para jugar:</p>
            <v-text-field v-model.number="monto" label="Monto inicial" type="number" min="1" dense outlined required />
            <v-btn @click="confirmarNuevoUsuario" color="success" block>
                Continuar
            </v-btn>
        </div>
    </v-card>
</template>

<script setup>
import { ref } from 'vue'
import { storeToRefs } from 'pinia'
import { rouletteStore } from '@/store/rouletteStore'

const store = rouletteStore()
const { usuario, errorUsuario } = storeToRefs(store)

const nombre = ref('')
const monto = ref(null)
const buscado = ref(false)

const emit = defineEmits(['usuario-logeado'])

const resetState = () => {
    errorUsuario.value = null
    usuario.value = null
    buscado.value = false
}

const handleSubmit = async () => {
    try {
        await store.buscarUsuario(nombre.value)
        buscado.value = true
    } catch (error) {
        errorUsuario.value = error
        buscado.value = true
    }
}

const confirmarNuevoUsuario = () => {
    if (!monto.value || monto.value <= 0) return
    store.usuario = {
        nombre: nombre.value,
        saldo: monto.value
    }
    store.errorUsuario = null
    emitirUsuario()
}

const emitirUsuario = () => {
    emit('usuario-logeado', store.usuario)
}
</script>

<style scoped>
.v-btn:disabled {
    background-color: #4CAF50 !important;
    color: white !important;
    filter: brightness(80%);
    cursor: not-allowed;
}
</style>
