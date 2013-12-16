$(function() {
  var communicator = {};
   
  $.Comm = function( id, stream ) {
    var callbacks, method,
      comm = id && communicator[ id + '.' + stream ];
   
    if ( !comm ) {
      callbacks = $.Callbacks();
      comm = {
        publish: callbacks.fire,
        subscribe: callbacks.add,
        unsubscribe: callbacks.remove
      };
      if ( id ) {
        communicator[ id + '.' + stream ] = comm;
      }
    }
    return comm;
  };
});