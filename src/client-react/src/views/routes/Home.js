import React from 'react';
import { Grid } from '@material-ui/core';
import Chart from '../../components/Chart';
import c3 from 'c3';
import { useSelector } from 'react-redux'
import { selectAnswersMap, selectAnswers } from '../../store/resourceSlice'
import { selectStatsMap, selectStats } from '../../store/surveySlice'


export default function Statistics(props) {
    const chartTitles = {
        '1': 'Planet',
        '2': 'Pet',
        '3': 'Continent',
        '4': 'Game Genre',
        '5': 'Music Genre',
        '6': 'Tech Company',
    };

    const answers = useSelector(selectAnswers);
    const answersMap = useSelector(selectAnswersMap);
    const statsMap = useSelector(selectStatsMap);
    const stats = useSelector(selectStats);

    const planetChart = (ref, data) => {
        let votes = [chartTitles[1]];
        let categories = [];

        data.forEach(i => {
            categories.push(answersMap[i.AnswerId]?.Name ?? 'N/A');
            votes.push(i.Count);
        });

        let chartData = [votes];

        return c3.generate({
            bindto: ref,
            data: {
                columns: chartData,
                type: 'bar',
                groups: [
                    [chartTitles[1]]
                ],
            },
            axis: {
                x: {
                    type: 'category',
                    categories: categories
                }
            }
        })
    }

    const gameChart = (ref, data) => {
        let votes = [chartTitles[4]];
        let categories = [];

        data.forEach(i => {
            categories.push(answersMap[i.AnswerId]?.Name ?? 'N/A');
            votes.push(i.Count);
        });

        let chartData = [votes];

        return c3.generate({
            bindto: ref,
            data: {
                columns: chartData,
                type: 'bar',
                groups: [
                    [chartTitles[4]]
                ],
            },
            axis: {
                x: {
                    type: 'category',
                    categories: categories
                }
            }
        })
    }

    const petChart = (ref, data) => {
        let chartData = [];

        data.forEach(i => {
            chartData.push([
                answersMap[i.AnswerId]?.Name ?? 'N/A',
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
                title: chartTitles[2]
            }
        });
    }

    const musicChart = (ref, data) => {
        let chartData = [];

        data.forEach(i => {
            chartData.push([
                answersMap[i.AnswerId]?.Name ?? 'N/A',
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
                title: chartTitles[5]
            }
        });
    }

    const continentChart = (ref, data) => {
        let chartData = [];

        data.forEach(i => {
            chartData.push([
                answersMap[i.AnswerId]?.Name ?? 'N/A',
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
                title: chartTitles[3]
            }
        });
    }

    const techChart = (ref, data) => {
        let chartData = [];

        data.forEach(i => {
            chartData.push([
                answersMap[i.AnswerId]?.Name ?? 'N/A',
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
                title: chartTitles[6]
            }
        });
    }

    return (
        <React.Fragment>
        {
            stats.length > 0 && answers.length > 0 ?
            (
                <React.Fragment>
                    <Grid container spacing={4}>
                        <Grid item xs={4}><Chart title={chartTitles[1]} implementation={planetChart} data={statsMap[1]} /></Grid>
                        <Grid item xs={4}><Chart title={chartTitles[2]} implementation={petChart} data={statsMap[2]} /></Grid>
                        <Grid item xs={4}><Chart title={chartTitles[3]} implementation={continentChart} data={statsMap[3]} /></Grid>
                    </Grid>
                    <Grid container spacing={4}>
                        <Grid item xs={4}><Chart title={chartTitles[4]} implementation={gameChart} data={statsMap[4]} /></Grid>
                        <Grid item xs={4}><Chart title={chartTitles[5]} implementation={musicChart} data={statsMap[5]} /></Grid>
                        <Grid item xs={4}><Chart title={chartTitles[6]} implementation={techChart} data={statsMap[6]} /></Grid>
                    </Grid>
                </React.Fragment>
            )
            : null
        }
        </React.Fragment>
    )
}