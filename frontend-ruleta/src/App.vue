<template>
  <div class="app-container">
    <h1>🎰 Ruleta de Casino</h1>

    <UserForm v-if="!usuario" @usuario-logeado="handleUserLoggedIn" />

    <div v-else>
      <RouletteWheel ref="rouletteWheelRef" @wheel-start="onWheelStart" @wheel-end="onWheelEnd" />
      <BetForm :usuario="usuario" :isSpinning="roulette.isSpinning" @bet-submitted="handleBet" />
    </div>

    <div v-if="roulette.currentBet" class="apuesta-box mx-auto mt-4">
      <div class="font-weight-bold mb-1">🎯 Apuesta actual:</div>

      <div v-if="roulette.currentBet.tipo === 'numero'">
        Número: <strong>{{ roulette.currentBet.valor }}</strong>
      </div>

      <div v-if="roulette.currentBet.tipo === 'color'">
        Color: <strong>{{ roulette.currentBet.valor }}</strong>
      </div>

      <template v-if="roulette.currentBet.tipo === 'paridad_color'">
        <div>Color: <strong>{{ roulette.currentBet.valor.color }}</strong></div>
        <div>Paridad: <strong>{{ roulette.currentBet.valor.paridad }}</strong></div>
      </template>

      <div class="mt-1">
        Monto apostado: <strong>{{ roulette.currentBet.monto }}$</strong>
      </div>
    </div>

    <ResultDisplay />

  </div>
</template>

<script setup>
import { ref } from 'vue'
import { rouletteStore } from '@/store/rouletteStore'
import RouletteWheel from './components/RouletteWheel.vue'
import BetForm from './components/BetForm.vue'
import UserForm from './components/UserForm.vue'
import ResultDisplay from './components/ResultDisplay.vue'

const lastResult = ref(null)
const roulette = rouletteStore()
const usuario = ref(null)

// Referencia a RouletteWheel
const rouletteWheelRef = ref(null)

const onWheelStart = (item) => {
  console.log('La ruleta comenzó?:', item)
  lastResult.value = null
  roulette.startSpin()
}

const onWheelEnd = async (item) => {
  console.log('La ruleta terminó:', item)
  lastResult.value = {
    name: item.id,
    color: item.background
  }
  roulette.stopSpin()

  await roulette.apostar(item)
}

const handleBet = (bet) => {
  roulette.setBet(bet)
  onWheelStart(bet)

  // Lanza la ruleta desde la referencia
  rouletteWheelRef.value?.launchWheel()
}

const handleUserLoggedIn = (userData) => {
  usuario.value = userData
}
</script>

<style scoped>
.app-container {
  text-align: center;
  padding: 2rem;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.border-right-md {
  border-right: 1px solid #ccc;
}

@media (max-width: 959px) {
  .border-right-md {
    border-right: none;
    border-bottom: 1px solid #ccc;
    margin-bottom: 1rem;
  }
}

.apuesta-box {
  background-color: #0094ff;
  color: white;
  max-width: 400px;
  padding: 16px;
  border-radius: 8px;
  border: 1px solid #007acc;
  text-align: center;
  font-size: 16px;
}

</style>
