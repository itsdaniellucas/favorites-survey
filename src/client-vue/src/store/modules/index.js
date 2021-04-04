import moduleTypes from '@/store/types/moduleTypes'
import alertModule from '@/store/modules/alertModule'
import surveyModule from '@/store/modules/surveyModule'
import resourceModule from '@/store/modules/resourceModule'

const modules = {
    [moduleTypes.Survey]: surveyModule,
    [moduleTypes.Alert]: alertModule,
    [moduleTypes.Resource]: resourceModule,
}

export default modules