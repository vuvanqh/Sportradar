import { useContext } from "react";
import { ModalContext } from "../store/ModalContext"
import EventModal from "./EventModal";
import CreateFFAEvent from "./CreateEvent/CreateFFAEventModal";
import CreateOneOnOneEvent from "./CreateEvent/CreateOneOnOneEventModal";
import CreateTeamEventModal from "./CreateEvent/CreateTeamEventModal";

export default function ModalRoot() {
  const { edit, view,createType, event, closeView ,closeEdit} = useContext(ModalContext);
  if(view && !edit && event)
    return <EventModal open event={event!} onClose={closeView}/>
  else if(edit && createType!=null && !view){
    switch(createType){
        case "FreeForAll": return <CreateFFAEvent open={edit} onClose={closeEdit}/>
        case "OneOnOne": return <CreateOneOnOneEvent open={edit} onClose={closeEdit}/>
        case "Team": return <CreateTeamEventModal open={edit} onClose={closeEdit}/>
    }
  }
  else 
    return null;
}