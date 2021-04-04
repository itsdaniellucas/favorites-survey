<template>
    <v-alert transition="fade-transition"
            dense
            :color="color"
            class="status-bar"
            v-show="visible">
        <v-progress-circular v-if="isProgress" :size="18" :width="2" indeterminate color="white" />
        <v-icon v-if="icon && !isProgress" :class="textClassObj">{{ icon }}</v-icon>
        <span class="ml-2" :class="textClassObj">{{ text }}</span>
    </v-alert>
</template>

<script>
    import modules from '@/store/types/moduleTypes'
    import { mapState } from 'vuex'

    export default {
        name: 'AlertBox',

        data: () => ({

        }),

        computed: {
            ...mapState(modules.Alert, [
                'visible',
                'color',
                'isProgress',
                'icon',
                'text',
                'isWhiteText',
            ]),
            textClassObj() {
                return {
                    'white-text': this.isWhiteText,
                    'black-text': !this.isWhiteText,
                }
            }
        }
    }
</script>

<style scoped>
.status-bar {
    width: auto;
    max-width: 300px;
    position: fixed !important;
    bottom: 5px;
    right: 20px;
}

.white-text {
    color: #fff;
}

.black-text {
    color: #000;
}
</style>