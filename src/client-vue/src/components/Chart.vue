<template>
    <v-sheet min-height="45vh"
                max-height="45vh"
                rounded="lg"
                style="overflow: hidden;"
                v-resize="chartResize">
        <v-row>
            <v-col cols="12">
                <h2 class="ml-3">{{ title }}</h2>
            </v-col>
        </v-row>
        <v-divider />
        <v-row>
            <v-col cols="12">
                <div ref="chart" />
            </v-col>
        </v-row>
    </v-sheet>
</template>

<script>
    export default {
        name: 'Chart',

        props: {
            title: {
                type: String,
                default: ''
            },
            implementation: {
                type: Function,
                default: () => {}
            },
            chartData: {
                type: Array,
                default: () => []
            }
        },

        data: () => ({
            chart: null,
        }),

        watch: {
            chartData() {
                this.$nextTick(() => {
                    this.createChart();
                })
            },
        },

        methods: {
            createChart() {
                if(this.chart) {
                    this.chart.unload();
                }
                this.chart = null;

                if(this.implementation) {
                    this.chart = this.implementation(this.$refs.chart, this.chartData);
                }
            },
            chartResize() {
                this.$nextTick(() => {
                    if(this.chart) {
                        if(this.$vuetify.breakpoint.name == 'lg') {
                            this.chart.resize({ height: 350 });
                        } else {
                            this.chart.resize({ height: 250 });
                        }
                    }
                })
            }
        },

        mounted() {
            this.$nextTick(() => {
                this.createChart();
            })
        },

        destroyed() {
            this.$nextTick(() => {
                if(this.chart) {
                    this.chart.unload();
                }
                this.chart = null;
            });
        }
    }
</script>

<style scoped>

</style>