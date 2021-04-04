import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Divider, Button, IconButton, Dialog, DialogActions, DialogTitle, DialogContent, LinearProgress, Chip, Grid, FormControl, FormControlLabel, FormLabel, Radio, RadioGroup } from '@material-ui/core';
import HowToVoteIcon from '@material-ui/icons/HowToVote';
import BeenhereIcon from '@material-ui/icons/Beenhere';
import KeyboardArrowRightIcon from '@material-ui/icons/KeyboardArrowRight';
import KeyboardArrowLeftIcon from '@material-ui/icons/KeyboardArrowLeft';
import { selectQuestions, selectAnswers, selectQuestionsMap, selectQuestionAnswersMap } from '../../store/resourceSlice'
import { useSelector } from 'react-redux'

const useStyles = makeStyles((theme) => ({
    content: {
        paddingTop: theme.spacing(2),
        paddingBottom: theme.spacing(2),
    },
    progress: {
        marginTop: theme.spacing(2),
    },
    question: {
        marginTop: theme.spacing(2),
    }
}));

export default function VoteDialog(props) {
    const classes = useStyles();
    const dialogWidth = 'sm';

    const questions = useSelector(selectQuestions);
    const answers = useSelector(selectAnswers);
    const questionsMap = useSelector(selectQuestionsMap);
    const questionAnswersMap = useSelector(selectQuestionAnswersMap);
   
    const [visible, setVisible] = useState(false);
    const [current, setCurrent] = useState(1);

    const getResponseDefaults = () => {
        if(questions.length == 0 || answers.length == 0)
            return [];

        return questions.map(i => {
            let firstAnswerId = 0;
            if(questionAnswersMap[i.Id][0]) {
                firstAnswerId = questionAnswersMap[i.Id][0].Id;
            }

            return {
                QuestionId: i.Id,
                AnswerId: firstAnswerId,
            }
        });
    }

    const [response, setResponse] = useState(getResponseDefaults());

    useEffect(() => {
        setResponse(getResponseDefaults());
    }, [questions, answers]);

    const question = questions.length > 0 ? questionsMap[current].Name : 'N/A';
    const total = questions?.length || 1;
    const progress = Math.round((current / total) * 100);

    const handleOpen = () => {
        setVisible(true);
    }

    const handleClose = () => {
        setVisible(false);
        setCurrent(1);
        setResponse(getResponseDefaults());
    }

    const onRadioChange = (event) => {
        let resCopy = [...response];
        resCopy[current - 1].AnswerId = parseInt(event.target.value);
        setResponse(resCopy);
    }

    const handleSave = () => {
        if(props.onSave) {
            props.onSave(response);
        }
        handleClose();
    }

    const onFirstQuestion = () => current == 1;
    const onLastQuestion = () =>  current == total;

    const next = () => {
        if(!onLastQuestion()) {
            setCurrent(current + 1);
        }
    }

    const previous = () => {
        if(!onFirstQuestion()) {
            setCurrent(current - 1);
        }
    }

    return (
        <React.Fragment>
            <IconButton className={props.className} onClick={handleOpen} disabled={props.disabled}>
                <HowToVoteIcon />
            </IconButton>
            <Dialog open={visible}
                    onClose={handleClose}
                    maxWidth={dialogWidth}
                    fullWidth>
                <DialogTitle>Vote</DialogTitle>
                <Divider />
                <DialogContent className={classes.content}>
                    <Grid container spacing={4}>
                        <Grid item xs={10}><LinearProgress className={classes.progress} color="primary" variant="determinate" value={progress} /></Grid>
                        <Grid item xs={2}><Chip color="primary" icon={<BeenhereIcon />} label={`${current} / ${total}`} /></Grid>
                    </Grid>

                    {
                        questions.length > 0 && answers.length > 0 && response.length > 0 ?
                        (
                            <FormControl className={classes.question} component="fieldset">
                                <FormLabel component="legend">{question}</FormLabel>
                                <RadioGroup value={response[current - 1].AnswerId} onChange={onRadioChange}>
                                    {
                                        answers.length > 0 ?
                                        questionAnswersMap[current].map(i => <FormControlLabel value={i.Id} key={i.Id} control={<Radio />} label={i.Name} />)
                                        : null
                                    }
                                </RadioGroup>
                            </FormControl>
                        ) : null
                    }

                    <Grid container spacing={4} justify="flex-end">
                        <Grid item xs={3}>
                            <IconButton color="primary" onClick={previous} disabled={onFirstQuestion()}>
                                <KeyboardArrowLeftIcon />
                            </IconButton>
                            <IconButton color="primary" onClick={next} disabled={onLastQuestion()}>
                                <KeyboardArrowRightIcon />
                            </IconButton>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="secondary">
                        Cancel
                    </Button>
                    <Button onClick={handleSave} color="primary" autoFocus disabled={!onLastQuestion()}>
                        Save
                    </Button>
                </DialogActions>
            </Dialog>
        </React.Fragment>
    )
}