<template>
    <div v-if="resultado" :class="['resultado-box', resultado.gano ? 'gano' : 'perdio']"
        class="texto-centrado mx-auto mt-4">
        <div class="icono-cabecera">
            {{ resultado.gano ? '🃏' : '❌' }}
        </div>

        <div class="font-weight-bold mb-1">
            Resultado de la Apuesta:
        </div>

        <div class="font-weight-bold mb-1">
            {{ resultado.gano ? '¡Ganaste!' : 'Perdiste esta ronda' }}
        </div>

        <div v-if="resultado.gano">
            Premio ganado: <strong>${{ resultado.premio }}</strong>
        </div>

        <div>
            Número ganador: <strong>{{ resultado.resultado.numero }}</strong>
        </div>

        <div>
            Color ganador: <strong>{{ resultado.resultado.color }}</strong>
        </div>

        <div>
            ¿Es par?: <strong>{{ resultado.resultado.esPar ? 'Sí' : 'No' }}</strong>
        </div>

        <div class="mt-2">
            Saldo actualizado: <strong>${{ usuario?.saldo.toFixed(2) }}</strong>
        </div>
    </div>
</template>

<script setup>
import { computed } from 'vue'
import { rouletteStore } from '@/store/rouletteStore'

const store = rouletteStore()

const resultado = computed(() => store.resultadoApuesta)
const usuario = computed(() => store.usuario)
</script>

<style scoped>
.resultado-box {
    max-width: 400px;
    padding: 16px;
    border: 1px solid #ccc;
    border-radius: 8px;
    font-size: 16px;
    text-align: center;
}

.resultado-box.gano {
    background-color: #d4edda;
    color: #155724;
    border-color: #c3e6cb;
}

.resultado-box.perdio {
    background-color: #f8d7da;
    color: #721c24;
    border-color: #f5c6cb;
}

.texto-centrado {
    text-align: center !important;
}

.icono-cabecera {
    font-size: 2rem;
    margin-bottom: 0.5rem;
}
</style>
