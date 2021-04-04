import React, { useState, useRef, useLayoutEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Paper, Divider, Typography } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
    root: {
        minHeight: '45vh',
        maxHeight: '45vh',
        overflow: 'hidden',
    },
    title: {
        fontSize: 32,
        margin: theme.spacing(1),
        marginLeft: theme.spacing(2),
    }
}));


export default function Chart(props) {
    const classes = useStyles();
    const target = useRef(null);
    const [chart, setChart] = useState(null);

    const destroyChart = () => {
        if(chart) {
            chart.unload();
        }
        setChart(null);
    }

    const createChart = () => {
        destroyChart();

        if(props.implementation) {
            setChart(props.implementation(target.current, props.data));
        }
    }

    useLayoutEffect(() => {
        createChart();

        return destroyChart();
    }, [props.data]);


    return (
        <Paper elevation={4} className={classes.root}>
            <Typography className={classes.title} color="textPrimary">{props.title}</Typography>
            <Divider />
            <div ref={target} />
        </Paper>
    )
}