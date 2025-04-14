<template>
    <v-container class="text-center d-flex flex-column align-center justify-center" fluid>
        <v-card class="pa-4 d-flex flex-column align-center" color="#1e1e1e" max-width="500" elevation="6">
            <Roulette ref="wheel" :items="items" size="400" :duration="5" easing="ease" display-indicator display-border
                indicator-position="top" @wheel-start="onWheelStart" @wheel-end="onWheelEnd" />
            <!-- <v-btn class="mt-4" color="blue" @click="launchWheel">🎯 Girar Ruleta </v-btn> -->
        </v-card>
    </v-container>
</template>

<script setup>
import { ref } from 'vue'
import { Roulette } from 'vue3-roulette'
import { useRoulette } from '@/composables/useRoulette'

const wheel = ref(null)
const { items } = useRoulette()

const emit = defineEmits(['wheel-start', 'wheel-end'])

const launchWheel = () => {
    wheel.value.launchWheel()
}

const onWheelStart = (item) => {
    emit('wheel-start', item)
}

const onWheelEnd = (item) => {
    emit('wheel-end', item)
}

defineExpose({ launchWheel })
</script>
