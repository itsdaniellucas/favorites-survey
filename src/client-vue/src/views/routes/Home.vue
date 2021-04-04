<template>
    <div v-if="stats.length > 0 && answers.length > 0">
        <v-row>
            <v-col cols="4">
                <Chart :title="chartTitles[1]" :implementation="planetChart" :chart-data="statsMap[1]" />
            </v-col>
            <v-col cols="4">
                <Chart :title="chartTitles[2]" :implementation="petChart" :chart-data="statsMap[2]" />
            </v-col>
            <v-col cols="4">
                <Chart :title="chartTitles[3]" :implementation="continentChart" :chart-data="statsMap[3]" />
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="4">
                <Chart :title="chartTitles[4]" :implementation="gameChart" :chart-data="statsMap[4]" />
            </v-col>
            <v-col cols="4">
                <Chart :title="chartTitles[5]" :implementation="musicChart" :chart-data="statsMap[5]" />
            </v-col>
            <v-col cols="4">
                <Chart :title="chartTitles[6]" :implementation="techChart" :chart-data="statsMap[6]" />
            </v-col>
        </v-row>
    </div>
</template>

<script>
    import Chart from '@/components/Chart'
    import c3 from 'c3'
    import { mapGetters, mapState } from 'vuex'
    import modules from '@/store/types/moduleTypes'

    export default {
        name: 'Home',

        components: {
            Chart,
        },

        data: () => ({
            chartTitles: {
                '1': 'Planet',
                '2': 'Pet',
                '3': 'Continent',
                '4': 'Game Genre',
                '5': 'Music Genre',
                '6': 'Tech Company',
            },
        }),

        computed: {
            ...mapState(modules.Resource, [
                'answers',
            ]),
            ...mapState(modules.Survey, [
                'stats',
            ]),
            ...mapGetters(modules.Resource, [
                'questionsMap',
                'answersMap'
            ]),
            ...mapGetters(modules.Survey, [
                'statsMap'
            ]),
        },

        methods: {
            planetChart(ref, data) {
                let votes = [this.chartTitles[1]];
                let categories = [];

                data.forEach(i => {
                    categories.push(this.answersMap[i.AnswerId]?.Name ?? 'N/A');
                    votes.push(i.Count);
                });

                let chartData = [votes];

                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'bar',
                        groups: [
                            [this.chartTitles[1]]
                        ],
                    },
                    axis: {
                        x: {
                            type: 'category',
                            categories: categories
                        }
                    }
                })
            },

            gameChart(ref, data) {
                let votes = [this.chartTitles[4]];
                let categories = [];

                data.forEach(i => {
                    categories.push(this.answersMap[i.AnswerId]?.Name ?? 'N/A');
                    votes.push(i.Count);
                });

                let chartData = [votes];

                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'bar',
                        groups: [
                            [this.chartTitles[4]]
                        ],
                    },
                    axis: {
                        x: {
                            type: 'category',
                            categories: categories
                        }
                    }
                })
            },

            petChart(ref, data) {
                let chartData = [];

                data.forEach(i => {
                    chartData.push([
                        this.answersMap[i.AnswerId]?.Name ?? 'N/A',
                        i.Count
                    ]);
                });


                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'donut',
                    },
                    donut: {
                        title: this.chartTitles[2]
                    }
                });
            },

            musicChart(ref, data) {
                let chartData = [];

                data.forEach(i => {
                    chartData.push([
                        this.answersMap[i.AnswerId]?.Name ?? 'N/A',
                        i.Count
                    ]);
                });

                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'donut',
                    },
                    donut: {
                        title: this.chartTitles[5]
                    }
                });
            },

            continentChart(ref, data) {
                let chartData = [];

                data.forEach(i => {
                    chartData.push([
                        this.answersMap[i.AnswerId]?.Name ?? 'N/A',
                        i.Count
                    ]);
                });


                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'pie',
                    },
                    donut: {
                        title: this.chartTitles[3]
                    }
                });
            },

            techChart(ref, data) {
                let chartData = [];

                data.forEach(i => {
                    chartData.push([
                        this.answersMap[i.AnswerId]?.Name ?? 'N/A',
                        i.Count
                    ]);
                });


                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'pie',
                    },
                    donut: {
                        title: this.chartTitles[6]
                    }
                });
            },
        },
    }
</script>

<style scoped>

</style>